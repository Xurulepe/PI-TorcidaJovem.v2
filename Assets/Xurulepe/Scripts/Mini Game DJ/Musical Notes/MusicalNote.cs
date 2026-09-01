using UnityEngine;

public class MusicalNote : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private RectTransform rectTransform;

    private NoteDirection noteDirection;
    private bool wasHit = false;
    private bool wasChecked = false;


    public bool WasHit => wasHit;
    public bool WasChecked => wasChecked;


    private void Update()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y -1f * moveSpeed * Time.deltaTime);
    }

    public void SetHit()
    {
        wasHit = true;
    }

    public void SetChecked()
    {
        wasChecked = true;
    }

    public void DeactiveSelf()
    {
        GameManager.Instance.IncrementDeactivatedNotesCount();
        GameManager.Instance.RemoveActiveNote(rectTransform, noteDirection);
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    public Vector2 GetPosition()
    {
        return rectTransform.anchoredPosition;
    }

    public void SetNoteDirection(NoteDirection noteDirection)
    {
        this.noteDirection = noteDirection;
    }

    private void OnDisable()
    {
        wasHit = false;
        wasChecked = false;
    }
}
