using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GettingRidOfIt
{
    public class PromptInteract : InteractableObject
    {
        private ActionsManager actionsManager;


        [Required]
        [SerializeField]
        private InteractItem interactitem;


        [Required]
        [InfoBox("All Action ScriptableObjects")]
        [SerializeField]
        List<InteractData> interactions = new();



        #region highlight shit
        [Space(20)]
        [HideIf("isDecal")]
        [HideIf("isMesh")]
        [SerializeField] bool isSprite = true;
        [HideIf("isSprite")]
        [HideIf("isMesh")]
        [Space(20)]
        [SerializeField] bool isDecal;

        [Space(20)]
        [HideIf("isSprite")]
        [HideIf("isDecal")]
        [SerializeField] bool isMesh;

        [Space(5)]
        [ShowIf("isSprite")]
        [HideIf("isDecal")]
        [HideIf("isMesh")]
        [SerializeField] SpriteRenderer[] spriteRenderers;
        [ShowIf("isSprite")]
        [HideIf("isDecal")]
        [HideIf("isMesh")]
        [SerializeField] private Color defaultColor, interactColor, highlightColor;

        [HideIf("isSprite")]
        [HideIf("isDecal")]
        [ShowIf("isMesh")]
        [SerializeField] MeshRenderer meshRenderer;
        [HideIf("isSprite")]
        [HideIf("isDecal")]
        [ShowIf("isMesh")]
        [SerializeField] Material defaultMat, interactMat, highlightMat;

        [Space(5)]
        [ShowIf("isDecal")]
        [SerializeField] private DecalProjector decalProjector;

        [ShowIf("isDecal")]
        [SerializeField] private Material decalHighlighMat, decalDefaultMat;


        [Title("outline for the object")]
        [SerializeField] private QuickOutline outline;


        public QuickOutline Outline { get => outline; }

        #endregion
        public override void Highlight(bool highlight)
        {
            if (highlight)
            {
                UIInteractableItemInfo.Instance.ShowInfo(interactitem);
            }
            else UIInteractableItemInfo.Instance.HideInfo();

            #region paskaa koodia
            if (isInteracting)
            {
                if (isSprite)
                {
                    foreach (var sprite in spriteRenderers)
                        sprite.color = defaultColor;
                }
                else if (isDecal)
                {
                    decalProjector.material = decalDefaultMat;
                }

                return;
            }
            #endregion
            hasHighlighted = highlight;

            if (isSprite)
            {
                foreach (var sprite in spriteRenderers)
                    sprite.color = highlight ? highlightColor : defaultColor;
            }
            else if (isDecal)
            {
                decalProjector.material = highlight ? decalHighlighMat : decalDefaultMat;
            }/*
            else if (isMesh)
            {
                meshRenderer.material = highlight ? highlightMat : defaultMat;
            }*/
        }


        public override void Interact(bool interact)
        {
            

            //set the interactions
            //jos on ainoastaan yksi interactio, niin ei tarvitse n‰ytt‰‰ valikkoa
            if (interactions.Count == 1 && interactions[0].canBeNotShown)
            {
                if (!interact) return;



                if (!interactions[0].IsAvailable(interactitem))
                    return;

                if (!interactions[0].CheckIfCanInteract(interactitem))
                {
                    DialogueManager.instance.StartDialogue(interactions[0].failDialogue);
                    return;
                }

                DialogueManager.instance.StartDialogue(interactions[0].ChooseDialogue());
                DoAction(interactions[0]);

                Destroy(gameObject);

                return;
            }



            try
            {
                actionsManager = UIManager.Instance.InteractPanel;

                if (interact)
                    actionsManager.EnableActions(interactions, interactitem);

                //actions manager sets all the buttons automativally based on the possible interactions.
                actionsManager.gameObject.SetActive(interact);


                Debug.Log($"prompt interact {interact}");

                isInteracting = interact;

                //when the player interacts with an object, the highlight gets disabled
                Highlight(false);
            }
            catch
            {
                Debug.Log("Some shit");
            }

            
        }


        private void FixedUpdate()
        {
            if (!isInteracting && !hasHighlighted)
            {
                if (isSprite)
                {
                    if (spriteRenderers[0].color != defaultColor)
                    {
                        foreach(var sprite in spriteRenderers)
                            sprite.color = defaultColor;
                    }
                }
                else if(isDecal)
                {
                    if (decalProjector.material != decalDefaultMat)
                        decalProjector.material = decalDefaultMat;
                }
            }

            if (!isMesh) return;
            //check if the player is close enough, and if it is, put the outline on if it's not null
            if(Vector3.Distance(Player.Instance.transform.position, transform.position) < 5)
            {
                if(outline != null)
                {
                    outline.enabled = true;
                }
            }
            else
            {
                if(outline != null)
                {
                    outline.enabled = false;
                }
            }
        }


        private void DoAction(InteractData iData)
        {
            if (iData.hasSound && iData.sound != null)
            {
                interactitem.audioSource.PlayOneShot(iData.sound);
            }

            //iData.onInteract?.Invoke(interactItem);
            iData.Interact(interactitem);

            if (iData.actionMinutes > 0) GameEvents.SkipTime(iData.actionMinutes);
        }
    }
}
