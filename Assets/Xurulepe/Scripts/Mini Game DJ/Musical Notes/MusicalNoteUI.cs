using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicalNoteUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float changeColorDuration = 0.5f;

    private Color originalColor;


    private void Awake()
    {
        originalColor = image.color;
    }

    public void Blink()
    {
        StartCoroutine(FlashColor());
    }

    private IEnumerator FlashColor()
    {
        image.color = GameManager.Instance.CurrentMusicalNoteData.noteSpriteColor;

        yield return new WaitForSeconds(changeColorDuration);

        image.color = originalColor;
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
