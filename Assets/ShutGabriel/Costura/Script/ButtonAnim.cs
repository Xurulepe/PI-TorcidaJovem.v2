using DG.Tweening;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float startOffset = 800f;

    private RectTransform rectTransform;
    private Vector2 targetPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition += Vector2.right * startOffset;
        rectTransform.DOAnchorPos(targetPosition, duration)
            .SetEase(Ease.OutCubic);
    }
}
