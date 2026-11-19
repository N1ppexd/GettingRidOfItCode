using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    public class StairInteract : InteractableObject
    {

        [Header("Teleport Position Transform")]
        [Tooltip("The transform, which position is used to teleport the player")]
        [SerializeField] Transform teleportPos;


        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [Title("MeshRenderer of stairs")]
        [HideLabel]
        [SerializeField] MeshRenderer stairMesh;

        #region Materials

        [TitleGroup("Materials")]
        [Space(20)]
        [Title("Normal Material")]
        [HideLabel]
        [SerializeField] Material objMaterial;

        [TitleGroup("Materials")]
        [Space(10)]
        [Title("Highlight Material")]
        [HideLabel]
        [SerializeField] Material highlightMaterial;

        [TitleGroup("Materials")]
        [Space(10)]
        [Title("Interact Material")]
        [HideLabel]
        [SerializeField] Material interactMaterial;

        #endregion


        //when the player highlights this object
        public override void Highlight(bool highlight)
        {
            hasHighlighted = highlight;

            if (objMaterial == null)
            {
                Debug.LogError("obj material is null");
            }

            if (highlight)
                stairMesh.material = highlightMaterial;

            else if (!highlight)
                stairMesh.material = objMaterial;
        }


        //When the player interacts with this object
        public override void Interact(bool interact)
        {
            isInteracting = interact;

            if (objMaterial == null)
            {
                Debug.LogError("obj material is null");
            }

            //if is interacting..
            if (interact)
            {
                stairMesh.material = interactMaterial;
            }
            else if(!interact)
            {
                stairMesh.material = objMaterial;
                hasHighlighted = false;
            }

            if(teleportPos == null) { Debug.LogError("teleport pos is null"); return; }

            Debug.Log($"stairs {transform.parent.name}");

            //teleport player to teleport position
            GameEvents.PlayerTeleport(teleportPos.position);
        }
    }
}
