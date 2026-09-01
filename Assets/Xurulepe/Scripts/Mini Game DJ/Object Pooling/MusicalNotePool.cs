using System.Collections.Generic;
using UnityEngine;

public class MusicalNotePool : MonoBehaviour
{
    public static MusicalNotePool Instance { get; private set; }

    [Header("Musical Notes Pooling System")]
    [SerializeField] private ObjectPool leftMusicalNote;
    [SerializeField] private ObjectPool downMusicalNote;
    [SerializeField] private ObjectPool upMusicalNote;
    [SerializeField] private ObjectPool rightMusicalNote;
    

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

    public GameObject GetPooledObject(NoteDirection noteDirection)
    {
        switch (noteDirection)
        {
            case NoteDirection.Left:
                return leftMusicalNote.GetPooledObject();
            
            case NoteDirection.Down:
                return downMusicalNote.GetPooledObject();

            case NoteDirection.Up:
                return upMusicalNote.GetPooledObject();

            case NoteDirection.Right:
                return rightMusicalNote.GetPooledObject();
        }

        return null;
    }
}

public enum NoteDirection
{
    Left,
    Down,
    Up,
    Right
}
