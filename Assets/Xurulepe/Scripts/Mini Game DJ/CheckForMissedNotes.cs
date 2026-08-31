using System.Collections.Generic;
using UnityEngine;

public class CheckForMissedNotes : MonoBehaviour
{
    [SerializeField] private float checkInterval;
    [SerializeField] private RectTransform rectTransform;

    private Rect rect;
    private float timer = 0f;
    [SerializeField] private List<RectTransform> allActiveMusicalNoteList = new List<RectTransform>();


    private void Start()
    {
        rect = GetWorldRect(rectTransform);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;

            UpdateList();
            Check();
        }
    }

    private void UpdateList()
    {
        allActiveMusicalNoteList.Clear();
        allActiveMusicalNoteList.AddRange(GameManager.Instance.GetActiveNotesList(MusicalNotePool.NoteType.Left));
        allActiveMusicalNoteList.AddRange(GameManager.Instance.GetActiveNotesList(MusicalNotePool.NoteType.Down));
        allActiveMusicalNoteList.AddRange(GameManager.Instance.GetActiveNotesList(MusicalNotePool.NoteType.Up));
        allActiveMusicalNoteList.AddRange(GameManager.Instance.GetActiveNotesList(MusicalNotePool.NoteType.Right));
    }

    private void Check()
    {
        for (int i = 0; i < allActiveMusicalNoteList.Count; i++)
        {
            RectTransform note = allActiveMusicalNoteList[i];

            if (note == null)
            {
                continue;
            }

            Rect noteRect = GetWorldRect(note);

            if (rect.Overlaps(noteRect))
            {
                MusicalNote musicalNote = note.GetComponent<MusicalNote>();
                
                if (!musicalNote.WasHit && !musicalNote.WasChecked)
                {
                    musicalNote.SetChecked();

                    GameManager.Instance.ReduceScore();

                    MusicalNoteUI musicalNoteUI = musicalNote.gameObject.GetComponent<MusicalNoteUI>();
                    musicalNoteUI.Blink();

                    Debug.LogWarning("passou");
                    return;
                }

                break;
            }
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
}
