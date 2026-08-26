using UnityEngine;

public class MusicalNoteSlot : MonoBehaviour
{
    [SerializeField] private LayerMask musicalNoteLayerMask;

    private MusicalNoteUI musicalNoteUI;
    private RectTransform rectTransform;

    private void Awake()
    {
        musicalNoteUI = GetComponent<MusicalNoteUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void CheckForNote()
    {
        Collider2D colliderObject = Physics2D.OverlapBox(rectTransform.anchoredPosition, Vector2.one, 0f, musicalNoteLayerMask);
        //RectTransformUtility.RectangleContainsScreenPoint(rectTransform, rectTransform.anchoredPosition);

        if (colliderObject != null)
        {
            MusicalNote musicalNote = colliderObject.GetComponent<MusicalNote>();

            if (musicalNote.WasHit)
            {
                return;
            }

            musicalNote.SetHit();

            float distanceToNote = Vector2.Distance(rectTransform.anchoredPosition, musicalNote.GetPosition());
            GameManager.Instance.CalculateScore(distanceToNote);

            //MusicalNoteUI musicalNoteUI = colliderObject.GetComponent<MusicalNoteUI>();
            musicalNoteUI.Blink();
            musicalNoteUI.Pulse();
            
            musicalNote.DeactiveSelf();
        }
        else
        {
            GameManager.Instance.ReduceScore();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
