using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GettingRidOfIt
{
    public class ShowTime : MonoBehaviour
    {
        [Required]
        [SerializeField] TextMeshProUGUI timeText;

        private void OnEnable()
        {
            GameEvents.OnPauseGame += PauseGame;
            GameEvents.OnUnPauseGame += UnpauseGame;
        }

        private void OnDisable()
        {
            GameEvents.OnPauseGame -= PauseGame;
            GameEvents.OnUnPauseGame -= UnpauseGame;
        }


        void PauseGame() => timeText.gameObject.SetActive(false);

        void UnpauseGame() => timeText.gameObject.SetActive(true);



        private void FixedUpdate()
        {
            if (timeText == null) return;

            if(GameManager.Instance == null) { Debug.LogError("GameManager is null"); return; } 

            if(GameManager.Instance.GameTime == null) { Debug.LogError("GameTime is null"); return; }

            SetTime(GameManager.Instance.GameTime);
        
        
        }

        /// <summary>
        /// Set the time visible in the UI
        /// </summary>
        /// <param name="gameTime"></param>
        private void SetTime(GameTime gameTime)
        {
            timeText.text = null;
            
            string dayString = $"Day {gameTime.Days}, ";
            string hourString = gameTime.Hours < 10 ? $"0{gameTime.Hours}:" : gameTime.Hours.ToString() + ":";
            string minuteString = gameTime.Minutes > 9 ? gameTime.Minutes.ToString() : $"0{gameTime.Minutes}";

            timeText.text = dayString + hourString + minuteString;
        }

    }
}
