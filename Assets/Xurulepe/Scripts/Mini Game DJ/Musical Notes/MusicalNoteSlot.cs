using System.Collections.Generic;
using UnityEngine;

public class MusicalNoteSlot : MonoBehaviour
{
    [SerializeField] private LayerMask musicalNoteLayerMask;
    [SerializeField] private MusicalNotePool.NoteType noteType;
    [SerializeField] private RectTransform rectTransform;

    private MusicalNoteUI musicalNoteUI;

    private List<RectTransform> activeMusicalNoteList;

    private void Awake()
    {
        musicalNoteUI = GetComponent<MusicalNoteUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        activeMusicalNoteList = GameManager.Instance.GetActiveNotesList(noteType);
    }

    public void CheckForNote()
    {
        //Debug.Log("Is rectTransform null? " + (rectTransform == null));
        Rect rect = GetWorldRect(rectTransform);

        bool isNoteInSlot = false;

        MusicalNote musicalNote = null;

        for (int i = 0; i < activeMusicalNoteList.Count; i++)
        {
            RectTransform note = activeMusicalNoteList[i];

            if (note == null)
            {
                continue;
            }

            if (rect.Overlaps(GetWorldRect(note)))
            {
                isNoteInSlot = true;
                musicalNote = note.GetComponent<MusicalNote>();
                break;
            }
        }

        if (isNoteInSlot)
        {
            Debug.Log("Note in slot");
                        
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

    private Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        //Debug.Log("Is local rectTransform null? " + (rectTransform == null));
        ////Debug.Log("RectTransform: " + rectTransform.name);
        //Debug.Log("Vector3 array length: " + corners.Length);
        rectTransform.GetWorldCorners(corners);

        float x = corners[0].x;
        float y = corners[0].y;

        float width = corners[2].x - corners[0].x;
        float height = corners[2].y - corners[0].y;

        return new Rect(x, y, width, height);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
