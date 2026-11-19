using UnityEngine;
using UnityEngine.SceneManagement;

namespace GettingRidOfIt
{
    public class GameSceneManager : MonoBehaviour
    {
        [Header("Is this a game scene?")]
        [SerializeField] private bool isGameScene;

        [Space(5)]
        [Header("UI Scene")]
        [SerializeField]
        private string uiScene;

        [Space(5)]
        [Header("Environment Scene")]
        [SerializeField]
        private string environmentScene;

        private void Awake()
        {
            if(isGameScene)
            {
                SceneManager.LoadScene(uiScene, LoadSceneMode.Additive);
                SceneManager.LoadScene(environmentScene, LoadSceneMode.Additive);
            }
        }
    }
}
