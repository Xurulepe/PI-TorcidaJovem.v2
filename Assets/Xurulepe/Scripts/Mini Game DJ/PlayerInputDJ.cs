using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDJ : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MusicalNoteSlot leftMusicalNote;
    [SerializeField] private MusicalNoteSlot rightMusicalNote;
    [SerializeField] private MusicalNoteSlot downMusicalNote;
    [SerializeField] private MusicalNoteSlot upMusicalNote;

    private Vector2 input;

    public void SetInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            input = context.ReadValue<Vector2>();

            PlayMusicalNote(input); 
        }
    }

    private void PlayMusicalNote(Vector2 value)
    {
        if (value.x == -1)
        {
            leftMusicalNote.CheckForNote();
        }
        if (value.x == 1)
        {
            rightMusicalNote.CheckForNote();
        }
        if (value.y == -1)
        {
            downMusicalNote.CheckForNote();
        }
        if (value.y == 1)
        {
            upMusicalNote.CheckForNote();
        }
    }
}
