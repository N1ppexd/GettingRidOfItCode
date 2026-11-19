using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace GettingRidOfIt
{
    public class InteractAction : MonoBehaviour
    {

        [Title("The data of this action")]
        public InteractData interactData;

        [Space(10)]
        [Title("Action UI Text")]
        [SerializeField]
        protected TextMeshProUGUI actionText;

        [SerializeField] protected AudioSource audioSource;

        private bool canInteract = true;

        private InteractItem interactItem;

        #region initialization

        private void OnDisable()
        {
            if(interactData == null)
            {
                Debug.LogWarning("interactData is null");
                return;
            }
            //disable listeners
            //interactData.RemoveListeners();
        }
        #endregion



        /// <summary>
        /// Check if action can be performed
        /// </summary>
        /// <returns></returns>
        public bool CanUseAction(InteractData interactData, InteractItem interactItem)
        {
            this.interactItem = interactItem;
            this.interactData = interactData;

            //interactData.AddListeners();
            //canInteract = (bool)interactData.canInteract?.Invoke(interactItem);

            canInteract = interactData.IsAvailable(interactItem);


            //set the name in the ui
            actionText.text = interactData.actionName;

            return canInteract;
        }



        
        public void StartAction(Canvas interactCanvas)
        {
            //set the canvas disabled so it's not visible
            interactCanvas.enabled = false;

            canInteract = interactData.CheckIfCanInteract(interactItem);

            if (interactData.hasDialogue)
            {
                if(!canInteract)
                    DialogueManager.instance.StartDialogue(interactData.failDialogue);
                else
                    DialogueManager.instance.StartDialogue(interactData.ChooseDialogue());

            }
            

            if(canInteract)
                DoAction(interactData);

            //sets it back visible
            interactCanvas.enabled = true;
            interactCanvas.gameObject.SetActive(false);
        }

        
        private void DoAction(InteractData iData)
        {
            if(iData.hasSound && iData.sound != null)
            {
                interactItem.audioSource.PlayOneShot(iData.sound);
            }

            //iData.onInteract?.Invoke(interactItem);
            iData.Interact(interactItem);

            if(iData.actionMinutes > 0) GameEvents.SkipTime(iData.actionMinutes);
        }

    }
}
