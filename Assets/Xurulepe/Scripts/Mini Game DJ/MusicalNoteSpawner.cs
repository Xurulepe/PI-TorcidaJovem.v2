using UnityEngine;

public class MusicalNoteSpawner : MonoBehaviour
{
    [SerializeField] private MusicalNotePool.NoteType noteType;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject musicalNote = MusicalNotePool.Instance.GetPooledObject(noteType);

            if (musicalNote != null)
            {
                musicalNote.transform.position = transform.position;
                musicalNote.SetActive(true);
            }
        }
    }
}
