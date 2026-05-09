using UnityEngine;

public class CheckForMissedNotes : MonoBehaviour
{
    [SerializeField] private int notesLayerMaskInt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == notesLayerMaskInt)
        {
            MusicalNote musicalNote = collision.gameObject.GetComponent<MusicalNote>();

            if (!musicalNote.WasHit)
            {
                GameManager.Instance.ReduceScore();
            }
        }
    }
}
