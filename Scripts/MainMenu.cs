using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GettingRidOfIt
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField]
        private string gameLevelName;


        [Required]
        [SerializeField]
        private GameObject settingsMenu;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(gameLevelName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void Settings(bool enabled)
        {
            settingsMenu.SetActive(enabled);
        }
    }
}
