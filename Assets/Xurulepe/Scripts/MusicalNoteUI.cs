using System.Collections;
using UnityEngine;

public class MusicalNoteUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

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
        spriteRenderer.color = Color.green;

        yield return new WaitForSeconds(1);

        spriteRenderer.color = originalColor;
    }
}
