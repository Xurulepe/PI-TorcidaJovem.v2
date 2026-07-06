using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MusicalNoteUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float changeColorDuration = 0.5f;

    private Color originalColor;

    private void Awake()
    {
        originalColor = spriteRenderer.color;
    }

    public void Blink()
    {
        StartCoroutine(FlashColor());
    }

    private IEnumerator FlashColor()
    {
        spriteRenderer.color = GameManager.Instance.CurrentMusicalNoteData.noteSpriteColor;

        yield return new WaitForSeconds(changeColorDuration);

        spriteRenderer.color = originalColor;
    }

    public void Pulse()
    {
        transform.DOKill();

        float targetScale = GameManager.Instance.CurrentMusicalNoteData.targetScale;
        float duration = GameManager.Instance.CurrentMusicalNoteData.pulseDuration;

        transform.localScale = Vector3.one;

        transform.DOScale(targetScale, duration).SetLoops(2, LoopType.Yoyo);
    }
}
