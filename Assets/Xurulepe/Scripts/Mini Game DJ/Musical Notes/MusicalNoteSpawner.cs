using UnityEngine;

public class MusicalNoteSpawner : MonoBehaviour
{
    [SerializeField] private MusicalNotePool.NoteType noteType;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;

    private float spawnTimer;

    private int maxNotesCount;
    private int spawnedNotesCount;

    private RectTransform rectTransform;


    private void Awake()
    {
        spawnTimer = Random.Range(minimumSpawnTime, maximumSpawnTime);
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        maxNotesCount = GameManager.Instance.MaxNotesToSpawn;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            GameObject musicalNote = MusicalNotePool.Instance.GetPooledObject(noteType);

            if (musicalNote != null)
            {
                //musicalNote.transform.position = transform.position;
                musicalNote.GetComponent<MusicalNote>().SetPosition(rectTransform.anchoredPosition);
                musicalNote.SetActive(true);

                spawnedNotesCount++;
            }

            spawnTimer = Random.Range(minimumSpawnTime, maximumSpawnTime);
        }

        if (spawnedNotesCount >= maxNotesCount)
        {
            gameObject.SetActive(false);
        }
    }
}
