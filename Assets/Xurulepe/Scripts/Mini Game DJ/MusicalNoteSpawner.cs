using UnityEngine;

public class MusicalNoteSpawner : MonoBehaviour
{
    [SerializeField] private MusicalNotePool.NoteType noteType;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;

    private float spawnTimer;

    private void Awake()
    {
        spawnTimer = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            GameObject musicalNote = MusicalNotePool.Instance.GetPooledObject(noteType);

            if (musicalNote != null)
            {
                musicalNote.transform.position = transform.position;
                musicalNote.SetActive(true);
            }

            spawnTimer = Random.Range(minimumSpawnTime, maximumSpawnTime);
        }
    }
}
