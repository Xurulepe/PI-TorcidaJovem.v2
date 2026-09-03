using UnityEngine;
using UnityEngine.EventSystems;

public class MusicalNoteClicker : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MusicalNoteSlot musicalNoteSlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        musicalNoteSlot.CheckForNote();
    }
}
