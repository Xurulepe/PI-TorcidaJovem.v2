using UnityEngine;
using UnityEngine.EventSystems;

public class MusicalNoteClicker : MonoBehaviour
{
    [SerializeField] private MusicalNoteSlot musicalNoteSlot;

    private void OnMouseDown()
    {
        musicalNoteSlot.CheckForNote();
    }
}
