using UnityEngine;
using UnityEngine.SceneManagement;

namespace GettingRidOfIt
{
    public class PauseScreen : MonoBehaviour
    {

        private void OnEnable()
        {
            Time.timeScale = 0;

            GameEvents.OnUnPauseGame += UnPause;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;

            GameEvents.OnUnPauseGame -= UnPause;
        }


        public void Resume() => GameEvents.UnPauseGame();
        public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        private void UnPause() => gameObject.SetActive(false);
        public void Quit() => Application.Quit();
        public void MainMenu() => SceneManager.LoadScene(0);
    }
}
