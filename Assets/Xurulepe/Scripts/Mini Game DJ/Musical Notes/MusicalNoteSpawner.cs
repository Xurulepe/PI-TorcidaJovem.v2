using UnityEngine;

public class MusicalNoteSpawner : MonoBehaviour
{
    [SerializeField] private NoteDirection noteDirection;
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
            GameObject musicalNoteObject = MusicalNotePool.Instance.GetPooledObject(noteDirection);

            if (musicalNoteObject != null)
            {
                MusicalNote musicalNote = musicalNoteObject.GetComponent<MusicalNote>();

                musicalNote.SetPosition(rectTransform.anchoredPosition);
                musicalNote.SetNoteDirection(noteDirection);

                GameManager.Instance.AddActiveNote(musicalNote.GetComponent<RectTransform>(), noteDirection);

                musicalNoteObject.SetActive(true);

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
