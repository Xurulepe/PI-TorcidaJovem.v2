using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame.TecInformatica
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject tableObject;
        [SerializeField] private GameObject startMenu;

        private void Awake()
        {
            tableObject.SetActive(false);
        }

        public void StartGame()
        {
            startMenu.SetActive(false);
            tableObject.SetActive(true);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}
