using System.Collections.Generic;
using UnityEngine;

public class MusicalNotePool : MonoBehaviour
{
    public static MusicalNotePool Instance;

    [Header("Musical Notes Pooling System")]
    [SerializeField] private ObjectPool leftMusicalNote;
    [SerializeField] private ObjectPool downMusicalNote;
    [SerializeField] private ObjectPool upMusicalNote;
    [SerializeField] private ObjectPool rightMusicalNote;

    public enum NoteType
    {
        Left, 
        Down,
        Up,
        Right
    }

    private void Awake()
    {
        Instance = this;

        InitPool();
    }

    private void InitPool()
    {
        leftMusicalNote.SetupPool();
        downMusicalNote.SetupPool();
        upMusicalNote.SetupPool();
        rightMusicalNote.SetupPool();
    }

    public GameObject GetPooledObject(NoteType noteType)
    {
        switch (noteType)
        {
            case NoteType.Left:
                return leftMusicalNote.GetPooledObject();
            
            case NoteType.Down:
                return downMusicalNote.GetPooledObject();

            case NoteType.Up:
                return upMusicalNote.GetPooledObject();

            case NoteType.Right:
                return rightMusicalNote.GetPooledObject();
        }

        return null;
    }
}
