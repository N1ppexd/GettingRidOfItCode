using N1ppeUtils;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GettingRidOfIt
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Required]
        [SerializeField] private Body body;


        [Space(10)]
        [TitleGroup("Game Time")]
        [InfoBox("This scales how much faster the game time is compared to real time")]
        [SerializeField] private float timeScaler = 1f;


        private GameTime gameTime;


        #region getters and setters
        public float SuspicionAmount { get; private set; }
        public Body BodyManager { get => body; }

        public GameTime GameTime { get => gameTime; }

        #endregion
        private void Awake()
        {
            //Singleton
            if (Instance == null) Instance = this;
            else Destroy(this);

            //Initialize gameTime
            gameTime = new GameTime(timeScaler);

            Cursor.lockState = CursorLockMode.None;
        }

        private void OnEnable()
        {
            GameEvents.OnPauseGame += PauseGame;
            GameEvents.OnUnPauseGame += UnpauseGame;

            GameEvents.OnActionPerformed += ActionPerformed;

            StartCoroutine(MoveTime());
        }

        private void OnDisable()
        {
            GameEvents.OnPauseGame -= PauseGame;
            GameEvents.OnUnPauseGame -= UnpauseGame;

            GameEvents.OnActionPerformed -= ActionPerformed;
        }


        private void PauseGame() => StopCoroutine(MoveTime());
        private void UnpauseGame() => StartCoroutine(MoveTime());

        /// <summary>
        /// Shitty code but whatever
        /// </summary>
        /// <returns></returns>
        IEnumerator MoveTime()
        {
            yield return NiilosExtensions.GetWait(60f / gameTime.TimeScaler);

            gameTime.Minutes++;

            StartCoroutine(MoveTime());
        }


        /// <summary>
        /// This is done so that we can change how much the suspicion is changed depening onthings like time, is the player bloody, etc.
        /// </summary>
        /// <param name="interactData"></param>
        private void ActionPerformed(InteractData interactData)
        {
            SuspicionChange(interactData.actionCost);

            gameTime.Minutes += interactData.actionMinutes;
        }

        private void Start()
        {
            if (body == null)
            {
                try
                {
                    body = Body.Instance;
                }
                catch
                {
                    Debug.LogError("Body is null");
                    return;
                }
            }

            StartCoroutine(RemainingUpdator());
        }


        void FixedUpdate()
        {
            if (SuspicionAmount >= 100)
                GameEvents.LoseGame();

            else if (body.BodyRemaining <= 0)
                GameEvents.WinGame();
        }

        /// <summary>
        /// Hyvin hyvin paskaa koodia, mutta on kiire
        /// </summary>
        /// <returns></returns>
        IEnumerator RemainingUpdator()
        {
            UIManager.Instance.SetBodyRemainingSlider(body.BodyRemaining);
            yield return NiilosExtensions.GetWait(0.2f);
            StartCoroutine(RemainingUpdator());
        }

        /// <summary>
        /// Change suspicion in <paramref name="amount"/> %
        /// </summary>
        /// <param name="amount"></param>
        public void SuspicionChange(float amount)
        {
            float changeAmount = amount;

            if (SuspicionAmount + changeAmount < 0)
                SuspicionAmount = 0;
            else if (SuspicionAmount + changeAmount > 100)
                SuspicionAmount = 100;
            else SuspicionAmount += changeAmount;

            UIManager.Instance.SetSuspicionSlider(SuspicionAmount);
        }
    }




    public class GameTime
    {

        private int days, hours, minutes;


        public int Days
        {
            get => days;
            set => days = value;
        }
        public int Hours 
        { 
            get => hours;
            set
            {
                if(value >= 24)
                {
                    Days++;
                    hours = 0;
                }
                else hours = value;
            }
        }
        public int Minutes 
        { 
            get => minutes;
            set
            {
                if (value >= 60)
                {
                    Hours++;
                    minutes = 0;

                    GameEvents.HourPassed();
                }
                else minutes = value;
            }
        }


        //This scales how much faster the game time is compared to real time
        public float TimeScaler { get; private set; }


        public GameTime(float timeScaler)
        {
            TimeScaler = timeScaler;
        }
    }
}
