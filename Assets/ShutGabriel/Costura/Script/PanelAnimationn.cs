using DG.Tweening;
using UnityEngine;

public class PanelAnimationn : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;

    private void Start()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(Vector3.one, duration)
            .SetEase(Ease.OutBack);
    }
}
