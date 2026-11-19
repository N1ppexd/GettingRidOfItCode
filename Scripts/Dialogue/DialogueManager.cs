using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using N1ppeUtils;
using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.Rendering;

namespace GettingRidOfIt
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;


        [SerializeField] private GameObject DialogueCanvas;
        [SerializeField] private TextMeshProUGUI dialogueText;

        [SerializeField] private float defaultTime = 0.5f;

        private bool isInDialogue;

        private List<IEnumerator> tasks = new List<IEnumerator>();

        private void Awake()
        {
            instance = this;
        }



        public void StartDialogue(string dialogue)
        {


            float delay = 0;

            if (tasks.Count > 0)
            {
                delay = 0.4f;

                foreach(IEnumerator task in tasks)
                {
                    StopCoroutine(task);
                }

                tasks.Clear();
            }


            IEnumerator coroutine = Dialogue(dialogue, delay);

            tasks.Add(coroutine);

            StartCoroutine(coroutine);

        }


        IEnumerator Dialogue(string dialogue, float delay)
        {
            yield return NiilosExtensions.GetWait(delay);
            if(DialogueCanvas == null)
            {
                Debug.Log("dialogue canvas will display: " + dialogue);
                yield break;
            }

            isInDialogue = true;

            DialogueCanvas.SetActive(true);
            dialogueText.text = "";

            float time = defaultTime;
            time /= dialogue.Length;

            foreach(char c in dialogue)
            {
                //seconds to millisecons
                yield return NiilosExtensions.GetWait(time);

                dialogueText.text += c;
            }

            yield return NiilosExtensions.GetWait(3f);


            isInDialogue = false;
            DialogueCanvas.SetActive(false);
        }
    }
}
