using N1ppeUtils;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GettingRidOfIt
{
    public class BodyPart : InteractItem
    {
        [Header("Sprite")]
        [SerializeField] protected SpriteRenderer[] spriteRenderers;

        [LabelText("Can Dismember?")]
        [HideLabel]
        public bool canDismember;


        [SerializeField] private ParticleSystem[] bloodParticles;


        [Space(10)]
        [EnableIf("canDismember")]
        [LabelText("Dismembered parts")]
        [HideLabel]
        [SerializeField]
        private List<GameObject> dismemberedParts;


        [Space(20)]
        [SerializeField] private float shakeTime;


        public HolderSO CurrentHolder { get; set; }


        private Player player;

        private void Awake()
        {
            //camera shake
            Player.Instance.ImpulseSource.GenerateImpulse();

            player = Player.Instance;
        }
        #region enable disable
        private void OnEnable()
        {
            GameEvents.OnHourPassed += HourPassed;
        }
        private void OnDisable()
        {
            GameEvents.OnHourPassed -= HourPassed;
        }

        #endregion


        /// <summary>
        /// When the hour has passed
        /// </summary>
        private void HourPassed()
        {
            float decomposerMultiplier = 1f;

            if(IsInHolder && CurrentHolder != null)
            {
                decomposerMultiplier = CurrentHolder.decompositionMultiplier;
            }

            GameManager.Instance.SuspicionChange(itemSo.decompositionPerHour * decomposerMultiplier);
        }

        /// <summary>
        /// Default interact in here is that the player picks it up..
        /// </summary>
        /// <param name="onOrOff"></param>
        public override void DefaultInteract(bool onOrOff)
        {
            interacted = onOrOff;

            player = Player.Instance;
            if (onOrOff)
            {
                foreach(var renderer in spriteRenderers)
                    renderer.gameObject.layer = 9;
                player.Inventory.AddItem(this);
            }
            else
            {
                foreach (var renderer in spriteRenderers)
                    renderer.gameObject.layer = 10;
                player.Inventory.RemoveItem(this);
            }

        }

        /// <summary>
        /// Dismember the bodypart
        /// </summary>
        public void Dismember()
        {
            if (!canDismember) return;


            Instantiate(audioSource, transform.position, transform.rotation).Play();

            //instantiates parts where the bigger part was
            foreach(var part in dismemberedParts)
            {
                Instantiate(part, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }


        private void Update()
        {
            if (interacted)
            {
                if (player == null) player = Player.Instance;

                if (player == null) { Debug.LogError("wtf? player is still null"); return; }
                if (player.HandTransform == null) { Debug.LogError("wtf? Player has no hand.."); return; }


                //tool laitetaan pelaajan käteen...
                transform.position = player.HandTransform.position;
            }
            
        }


        #region Holder
        Bleeding bloodStain;
        SpriteRenderer[] renderers;

        /// <summary>
        /// put the item in the holder
        /// </summary>
        public override void PutInItem()
        {
            if(bloodParticles != null)
            {
                foreach (var particle in bloodParticles)
                    particle.Stop();
            }


            if(gameObject.TryGetComponent(out bloodStain)){
                bloodStain.enabled = false;
            }

            //try to get all children that have spriterenderers, and disable then
            if(NiilosExtensions.TryGetAllChildren(transform, out renderers))
            {
                foreach(var renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
        }

        /// <summary>
        /// Take item from the holder
        /// </summary>
        public override void TakeOutItem()
        {
            if(bloodStain != null)
            {
                bloodStain.enabled = true;
            }


            if(renderers.Length > 0)
            {
                foreach(var renderer in renderers)
                {
                    renderer.enabled = true;
                }
            }
        }

        #endregion


        public override BodyPart GetBodyPart() { return this; }
        public override InteractItem GetItem() {  return this; }
        public override Body GetBody() { return null; }
        public override Holder GetHolder() { return null; }
    }
}
