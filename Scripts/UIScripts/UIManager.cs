using N1ppeUtils;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GettingRidOfIt
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }


        private InputManager inputActions;

        [Header("Body remaining slider")]
        [SerializeField] private Slider bodyRemainingSlider;

        [Required]
        [SerializeField] private Slider susSlider;

        [Required]
        [SerializeField] private ActionsManager interactPanel;

        [Required]
        [Space(20)]
        [SerializeField] private GameObject pauseScreen;


        [Required]
        [Space(20)]
        [SerializeField] private GameObject loseScreen;
        [Required]
        [SerializeField] private GameObject winScreen;


        [Required]
        [Space(20)]
        [SerializeField]
        private TextMeshProUGUI actionPerformedText;


        private bool paused;

        public ActionsManager InteractPanel { get => interactPanel; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            bodyRemainingSlider.maxValue = 100;
            susSlider.maxValue = 100;
        }

        private void OnEnable()
        {
            inputActions = new InputManager();
            inputActions.Enable();


            inputActions.UI.Cancel.performed += PauseGame;

            GameEvents.OnPauseGame += OnGamePaused;
            GameEvents.OnUnPauseGame += OnGameUnPaused;

            GameEvents.OnPlayerLost += LoseGame;
            GameEvents.OnPlayerWin += WinGame;

            GameEvents.OnActionPerformed += PerformAction;
        }

        private void OnDisable()
        {
            inputActions.UI.Cancel.performed -= PauseGame;

            GameEvents.OnPauseGame -= OnGamePaused;
            GameEvents.OnUnPauseGame -= OnGameUnPaused;

            GameEvents.OnPlayerLost -= LoseGame;
            GameEvents.OnPlayerWin -= WinGame;

            GameEvents.OnActionPerformed -= PerformAction;

            inputActions.Disable();
        }

        /// <summary>
        /// Pause the game
        /// </summary>
        /// <param name="context"></param>
        private void PauseGame(InputAction.CallbackContext context)
        {
            if(!paused)
            {
                GameEvents.PauseGame();
                pauseScreen.SetActive(true);
                paused = true;
            }
            else
            {
                GameEvents.UnPauseGame();
                
                paused = false;
            }
        }

        private void PerformAction(InteractData interactData)
        {
            StopAllCoroutines();
            actionPerformedText.text = "";

            actionPerformedText.gameObject.SetActive(true);

            if (interactData.actionCost == 0)
            {
                actionPerformedText.gameObject.SetActive(false);
                return;
            }

            Debug.Log("shit");

            actionPerformedText.text = interactData.actionCost < 0 ? 
                $"Suspicion reduced {interactData.actionCost}%" 
                : $"Suspicion raised {interactData.actionCost}%";

            if(interactData.actionMinutes > 0)
            {
                actionPerformedText.text += "\n" + interactData.actionMinutes + " minutes passed";
            }

            StartCoroutine(ActionText(actionPerformedText.gameObject));

        }

        IEnumerator ActionText(GameObject textObj)
        {
            yield return NiilosExtensions.GetWait(2f);

            textObj.SetActive(false);
        }

        private void OnGamePaused()
        {
            pauseScreen.SetActive(true);
            susSlider.gameObject.SetActive(false);
            bodyRemainingSlider.gameObject.SetActive(false);
        }

        private void OnGameUnPaused()
        {
            pauseScreen.SetActive(false);
            susSlider.gameObject.SetActive(paused);
            bodyRemainingSlider.gameObject.SetActive(true);
        }


        /// <summary>
        /// When player loses the game
        /// </summary>
        private void LoseGame()
        {
            loseScreen.SetActive(true);
        }

        private void WinGame()
        {
            winScreen.SetActive(true);
        }


        public void SetBodyRemainingSlider(float remaining) => bodyRemainingSlider.value = remaining;
        public void SetSuspicionSlider(float amount) => susSlider.value = amount;
    }
}