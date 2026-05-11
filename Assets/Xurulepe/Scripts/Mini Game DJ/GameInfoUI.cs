using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameInfoUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI accuracyInfoText;
    [SerializeField] private RectTransform accuracyInfoRectTransform;
    [SerializeField] private TextMeshProUGUI scoreInfoText;
    [SerializeField] private RectTransform scoreInfoRectTransform;
    [SerializeField] private TextMeshProUGUI comboCountInfoText;
    [SerializeField] private RectTransform comboInfoRectTransform;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        accuracyInfoText.text = GameManager.Instance.AccuracyInfo;
        Pulse(accuracyInfoRectTransform);

        scoreInfoText.text = GameManager.Instance.Score.ToString();
        Pulse(scoreInfoRectTransform);

        comboCountInfoText.text = GameManager.Instance.ComboCount.ToString() + " x";
        Pulse(comboInfoRectTransform);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChanged -= UpdateUI;        
    }

    private void Pulse(RectTransform target)
    {
        target.DOKill();

        target.localScale = Vector3.one;

        target.DOScale(1.2f, 0.08f).SetLoops(2, LoopType.Yoyo);
    }
}
