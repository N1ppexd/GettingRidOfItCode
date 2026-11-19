using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [HideInInspector]
        public bool hasHighlighted;
        [HideInInspector]
        public bool isInteracting;


        [Header("Holdbutton or click")]
        [Tooltip("Do you need to hold interact button, or click it once, and then click again to disable.")]
        public InteractMode mode;
        public abstract void Highlight(bool highlight);
        public abstract void Interact(bool interact);
    }


    public enum InteractMode
    {
        hold,   //hold = hold to interact
        click,   //click = click to interact
        prompt  //can only iterct from a prompt
    }
}
