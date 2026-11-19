using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    [System.Serializable]
    [InlineEditor]
    public abstract class InteractData : ScriptableObject
    {

        #region actionSettings

        [TabGroup("General Settings")]
        [InfoBox("Shown in the UI")]
        public string actionName;

        [TabGroup("General Settings")]
        [Space(20)]
        [Title("Description of the action. Shown in the UI")]
        [HideLabel]
        [TextArea]
        public string actionDescription;

        [TabGroup("General Settings")]
        [Space(20)]
        [InfoBox("How much suspicion this action arises. In percentages")]
        [Range(-100f, 100f)]
        public float actionCost;

        [TabGroup("General Settings")]
        [Space(5)]
        [DetailedInfoBox("Is this a final step?", "If this action gets rid of some part of the body. The amount is taken from the body item settings")]
        public bool doesGetRidOfSomething;


        [TabGroup("General Settings")]
        [Space(5)]
        [InfoBox("How much time this action takes in minutes")]
        public int actionMinutes;


        [TabGroup("General Settings")]
        [Space(5)]
        [InfoBox("If this action can be automatically done without showing it in the UI")]
        public bool canBeNotShown;


        #endregion


        #region dialogue

        [TabGroup("Dialogue settings")]
        [Space(20)]
        public bool hasDialogue;

        [Space(5)]
        [TabGroup("Dialogue settings")]
        [EnableIf("hasDialogue")]
        public bool showAlways;

        [Space(5)]
        [TabGroup("Dialogue settings")]
        [EnableIf("hasDialogue")]
        [ShowIf("hasDialogue")]
        [DisableIf("showAlways")]
        [MinValue(1)]
        [InfoBox("How many times this dialogue is shown since the first time, if it's not shown always")]
        public int showCount = 1;


        [TabGroup("Dialogue settings")]
        [EnableIf("hasDialogue")]
        [TextArea]
        [HideLabel]
        [GUIColor("#d9ffe3")]
        public List<string> dialogues = new();


        [TabGroup("Dialogue settings")]
        [Space(5)]
        [Title("Action Failure Dialogue")]
        [EnableIf("hasDialogue")]
        [GUIColor("#ffd9de")]
        [HideLabel]
        [TextArea]
        public string failDialogue;
        #endregion


        #region sound

        [Space(20)]
        [BoxGroup("Sound")]
        public bool hasSound;

        [BoxGroup("Sound")]
        [ShowIf("hasSound")]
        [Title("Action sound effect")]
        [HideLabel]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public AudioClip sound;

        #endregion


        /// <summary>
        /// Choose a dialoge from the succes list, so when a dialogue is succesful
        /// </summary>
        /// <returns></returns>
        public string ChooseDialogue()
        {
            if(dialogues.Count == 0)
            {
                Debug.LogError("No dialogues found");
                return null;
            }

            if(dialogues.Count == 1)
            {
                return dialogues[0];
            }

            //return a dialogue randomly from the list
            return dialogues[UnityEngine.Random.Range(0, dialogues.Count)];
        }


        /// <summary>
        /// Check if the interact action is still available
        /// Otherwise it's not shown
        /// </summary>
        /// <param name="interactItem"></param>
        /// <returns></returns>
        public abstract bool IsAvailable(InteractItem interactItem);
        public abstract bool CheckIfCanInteract(InteractItem interactItem);
        public virtual void Interact(InteractItem interactItem) => GameEvents.PerformAction(this);

        public virtual bool CheckAvailability(InteractItem[] items, ItemType itemType)
        {
            foreach (InteractItem i in items)
            {
                if (i.ItemSo.itemType == itemType)
                    return true;
            }

            return false;
        }
    }
}
