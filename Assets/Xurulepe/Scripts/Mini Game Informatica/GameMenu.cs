using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame.TecInformatica
{
    public class GameMenu : MonoBehaviour
    {
        [Header("Game Music")]
        [SerializeField] private AudioClip gameMusic;

        [Header("References")]
        [SerializeField] private GameObject tableObject;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject finalMenu;

        [Header("Scene Settings")]
        [SerializeField] private int sceneBuildIndex = 1;

        private void Awake()
        {
            tableObject.SetActive(false);
        }

        private void Start()
        {
            GameManager.Instance.OnGameFinished += ShowFinalMenu;

            AudioManager.Instance.PlayMusic(gameMusic);
        }

        private void ShowFinalMenu()
        {
            gameUI.SetActive(false);
            finalMenu.SetActive(true);
        }

        public void StartGame()
        {
            startMenu.SetActive(false);
            gameUI.SetActive(true);
            tableObject.SetActive(true);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackToSelectScene()
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
