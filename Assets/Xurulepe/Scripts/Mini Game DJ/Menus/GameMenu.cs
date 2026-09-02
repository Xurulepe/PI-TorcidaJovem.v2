using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [Header("Start Menu Settings")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject hudGame;
    [SerializeField] private GameObject musicalNotesContainer;
    [SerializeField] private TextMeshProUGUI maxScoreText;

    [Header("Final Menu Settings")]
    [SerializeField] private GameObject finalMenu;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI maxScoreFinalText;

    [Header("Scene Sttings")]
    [SerializeField] private int sceneBuildIndex = 1;


    private void Start()
    {
        maxScoreText.text += PlayerPrefs.GetInt("Max Score");

        GameManager.Instance.OnGameComplete += ShowFinalPanel;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameComplete -= ShowFinalPanel;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        hudGame.SetActive(true);
        musicalNotesContainer.SetActive(true);

        GameManager.Instance.StartGame();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToSelectScene()
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    private void ShowFinalPanel()
    {
        finalMenu.SetActive(true);
        hudGame.SetActive(false); 
        musicalNotesContainer.SetActive(false);

        UpdateFinalInfo();
    }

    private void UpdateFinalInfo()
    {
        playerScoreText.text += GameManager.Instance.Score;

        string maxScore = PlayerPrefs.GetInt("Max Score").ToString();
        maxScoreFinalText.text += maxScore;
    }
}
