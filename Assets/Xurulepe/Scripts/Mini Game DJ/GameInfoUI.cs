using TMPro;
using UnityEngine;

public class GameInfoUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI accuracyInfoText;
    [SerializeField] private TextMeshProUGUI scoreInfoText;
    [SerializeField] private TextMeshProUGUI comboCountInfoText;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        accuracyInfoText.text = GameManager.Instance.AccuracyInfo;
        scoreInfoText.text = GameManager.Instance.Score.ToString();
        comboCountInfoText.text = GameManager.Instance.ComboCount.ToString() + " x";
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChanged -= UpdateUI;        
    }
}
