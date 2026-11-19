using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GettingRidOfIt
{
    public class ActionInfo : MonoBehaviour
    {

        [Required]
        [SerializeField] TextMeshProUGUI timeText, getRidOfText, raiseSuspicionText;


        public void ShowInformation(InteractAction interactAction)
        {
            InteractData interactData = interactAction.interactData;

            if(timeText == null || getRidOfText == null || raiseSuspicionText == null)
            {
                Debug.LogError("ActionInfo: One or more of the text fields are null");
                return;
            }


            timeText.text = (int)(interactData.actionMinutes / 60) + "h " + interactData.actionMinutes % 60 + "m";

            getRidOfText.text = interactData.doesGetRidOfSomething ? interactData.actionCost + "%" : "0%";

            raiseSuspicionText.text = interactData.actionCost + "%";

        }


        public void ResetInformation()
        {
            timeText.text = "0h 0m";
            getRidOfText.text = "0%";
            raiseSuspicionText.text = "0%";
        }
    }
}
