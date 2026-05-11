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
        accuracyInfoText.text = GameManager.Instance.CurrentMusicalNoteData.accuracyInfo;
        accuracyInfoText.colorGradient = GameManager.Instance.CurrentMusicalNoteData.noteGradient;
        Pulse(accuracyInfoRectTransform, GameManager.Instance.CurrentMusicalNoteData);

        scoreInfoText.text = GameManager.Instance.Score.ToString();
        Pulse(scoreInfoRectTransform, GameManager.Instance.CurrentMusicalNoteData);

        comboCountInfoText.text = GameManager.Instance.ComboCount.ToString() + " x";
        Pulse(comboInfoRectTransform, GameManager.Instance.CurrentMusicalNoteData);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChanged -= UpdateUI;        
    }

    private void Pulse(RectTransform target, MusicalNoteDataSO musicalNoteData)
    {
        target.DOKill();

        float targetScale = musicalNoteData.targetScale;
        float duration = musicalNoteData.pulseDuration;

        target.localScale = Vector3.one;

        target.DOScale(targetScale, duration).SetLoops(2, LoopType.Yoyo);
    }
}
