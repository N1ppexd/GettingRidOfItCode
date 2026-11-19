using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    public abstract class DoorInteract : InteractItem
    {


        [TabGroup("Animation Settings")]
        [Space(20)]
        [Title("Frigde")]
        [Required]
        [SerializeField]
        protected Animator animator;

        [TabGroup("Animation Settings")]
        [Space(20)]
        [SerializeField]
        protected string closeAnim, openAnim;

        [TabGroup("Animation Settings")]
        [SerializeField]
        protected string openModeBool;

        public override void DefaultInteract(bool onOrOff)
        {
            interacted = onOrOff;

            //Set the bool in the animator either open or closed
            animator.SetBool(openModeBool, onOrOff);

            //animator.Play(onOrOff ? openAnim : closeAnim);
        }



        public override InteractItem GetItem() => this;
    }
}
