using UnityEngine;

public class MusicalNoteSlot : MonoBehaviour
{
    [SerializeField] private LayerMask musicalNoteLayerMask;

    public void CheckForNote()
    {
        Collider2D colliderObject = Physics2D.OverlapBox(transform.position, Vector2.one, 0f, musicalNoteLayerMask);

        if (colliderObject != null)
        {
            MusicalNote musicalNote = colliderObject.GetComponent<MusicalNote>();

            if (musicalNote.WasHit)
            {
                return;
            }

            musicalNote.SetHit();

            float distanceToNote = Vector2.Distance(transform.position, colliderObject.transform.position);
            GameManager.Instance.CalculateScore(distanceToNote);

            MusicalNoteUI musicalNoteUI = colliderObject.GetComponent<MusicalNoteUI>();
            musicalNoteUI.Blink();            
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
