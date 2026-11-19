using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public class ActionsManager : MonoBehaviour
    {
        [Required]
        [InfoBox("The original button that gets instantiated multiple times for all actions")]
        [SerializeField]
        private InteractAction originalAction;

        private List<InteractAction> actions = new();

        /// <summary>
        /// Set the Ui prompts.
        /// <paramref name="interactItem"/> is the item that we interact with
        /// </summary>
        /// <param name="actionDatas"></param>
        /// <param name="interactItem"></param>
        public void EnableActions(List<InteractData> actionDatas, InteractItem interactItem)
        {

            DisableActions(actions);
            actions.Clear();

            //add the original action as the first button
            for (int i = 0; i < actionDatas.Count; i++)
            {
                actions.Add(Instantiate(originalAction, originalAction.transform.parent));

                actions[i].gameObject.SetActive(actions[i].CanUseAction(actionDatas[i], interactItem));
            }

            originalAction.gameObject.SetActive(false);
        }

        void DisableActions(List<InteractAction> actions)
        {
            foreach(var action in actions)
            {
                action.gameObject.SetActive(false);
            }
        }
    }
}
