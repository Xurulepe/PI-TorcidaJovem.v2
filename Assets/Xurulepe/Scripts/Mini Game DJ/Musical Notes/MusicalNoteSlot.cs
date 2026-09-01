using System.Collections.Generic;
using UnityEngine;

public class MusicalNoteSlot : MonoBehaviour
{
    [SerializeField] private NoteDirection noteDirection;
    [SerializeField] private LayerMask musicalNoteLayerMask;
    
    private RectTransform rectTransform;
    private MusicalNoteUI musicalNoteUI;
    private List<RectTransform> activeMusicalNoteList =  new List<RectTransform>();


    private void Awake()
    {
        musicalNoteUI = GetComponent<MusicalNoteUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        activeMusicalNoteList = GameManager.Instance.GetActiveNotesList(noteDirection);
    }

    private void Update()  // testing
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
        }
    }

    public void CheckForNote()
    {
        Rect rect = GetWorldRect(rectTransform);

        float containerPositionY = RectTransformUtility.WorldToScreenPoint(null, rect.position).y;
        float notePositionY = 0f;

        bool isNoteInSlot = false;

        MusicalNote musicalNote = null;

        for (int i = 0; i < activeMusicalNoteList.Count; i++)
        {
            RectTransform note = activeMusicalNoteList[i];

            if (note == null)
            {
                continue;
            }

            Rect noteRect = GetWorldRect(note);

            if (rect.Overlaps(noteRect))
            {
                isNoteInSlot = true;
                musicalNote = note.GetComponent<MusicalNote>();
                notePositionY = RectTransformUtility.WorldToScreenPoint(null, noteRect.position).y;

                break;
            }
        }

        if (isNoteInSlot)
        {                        
            if (musicalNote.WasHit)
            {
                return;
            }

            musicalNote.SetHit();

            float distanceToNote = Mathf.Abs(containerPositionY - notePositionY);
            GameManager.Instance.CalculateScore(distanceToNote);

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
