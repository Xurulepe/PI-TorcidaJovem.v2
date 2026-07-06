using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame.TecInformatica
{
    public class GameMenu : MonoBehaviour
    {
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

        public void Quit()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}
