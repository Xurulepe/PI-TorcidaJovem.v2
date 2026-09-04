using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private int maxNotesToSpawn;

    [Header("Musical Notes Settings")]
    [SerializeField] private MusicalNoteDataSO perfectMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO goodMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO coolMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO missMusicalNoteData;


    private MusicalNoteDataSO currentMusicalNoteData;
    private int comboCount = 0;
    private int deactivatedNotesCount;
    private bool isGameRunning = false;
    private int maxScore;

    private List<RectTransform> activeLeftMusicalNoteList = new List<RectTransform>();
    private List<RectTransform> activeDownMusicalNoteList = new List<RectTransform>();
    private List<RectTransform> activeUpMusicalNoteList = new List<RectTransform>();
    private List<RectTransform> activeRightMusicalNoteList = new List<RectTransform>();
    private List<RectTransform> allActiveMusicalNoteList = new List<RectTransform>();


    public int Score => score;
    public int ComboCount => comboCount;
    public MusicalNoteDataSO CurrentMusicalNoteData => currentMusicalNoteData;
    public int MaxNotesToSpawn => maxNotesToSpawn;
    public int DeactivatedNotesCount => deactivatedNotesCount;
    public bool IsGameRunning => isGameRunning;


    public event Action OnScoreChanged;
    public event Action OnGameComplete;


    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
    }

    public void FinishGame()
    {
        OnGameComplete?.Invoke();
    }

    #region SCORE CONTROLLER
    public void CalculateScore(float distanceToNote)
    {
        if (distanceToNote <= perfectMusicalNoteData.timingWindow)
        {
            currentMusicalNoteData = perfectMusicalNoteData;
        }
        else if (distanceToNote <= goodMusicalNoteData.timingWindow)
        {
            currentMusicalNoteData = goodMusicalNoteData;
        }
        else
        {
            currentMusicalNoteData = coolMusicalNoteData;
        }

        comboCount++;

        SetScore(score + currentMusicalNoteData.scoreValue);
    }

    public void ReduceScore()
    {
        currentMusicalNoteData = missMusicalNoteData;

        comboCount = 0;

        SetScore(score + currentMusicalNoteData.scoreValue);
    }

    private void SetScore(int newScore)
    {
        score = newScore;

        if (score < 0)
        {
            score = 0;
        }

        OnScoreChanged?.Invoke();
    }

    private void SaveScore()
    {
        if (score > PlayerPrefs.GetInt("Max Score"))
        {
            PlayerPrefs.SetInt("Max Score", score);

            Debug.Log("New score: " + score);
        }
    }
    #endregion

    #region MUSICAL NOTE CONTROLLER
    public void IncrementDeactivatedNotesCount()
    {
        deactivatedNotesCount++;

        CheckForLastNote();
    }

    private void CheckForLastNote()
    {
        int spawnersQuantity = 4;

        if (deactivatedNotesCount >= maxNotesToSpawn * spawnersQuantity)
        {
            SaveScore();

            isGameRunning = false;

            float triggerGameCompleteDelay = currentMusicalNoteData.pulseDuration * 3;
            Invoke(nameof(FinishGame), triggerGameCompleteDelay);
        }
    }

    public void AddActiveMusicalNote(RectTransform noteRectTransform, NoteDirection noteDirection)
    {
        switch (noteDirection)
        {
            case NoteDirection.Left:
                activeLeftMusicalNoteList.Add(noteRectTransform);

                break;

            case NoteDirection.Down:
                activeDownMusicalNoteList.Add(noteRectTransform);

                break;

            case NoteDirection.Up:
                activeUpMusicalNoteList.Add(noteRectTransform);

                break;

            case NoteDirection.Right:
                activeRightMusicalNoteList.Add(noteRectTransform);

                break;

            default:

                break;
        }

        allActiveMusicalNoteList.Add(noteRectTransform);
    }

    public void RemoveActiveMusicalNote(RectTransform noteRectTransform, NoteDirection noteDirection)
    {
        switch (noteDirection)
        {
            case NoteDirection.Left:
                activeLeftMusicalNoteList.Remove(noteRectTransform);

                break;

            case NoteDirection.Down:
                activeDownMusicalNoteList.Remove(noteRectTransform);

                break;

            case NoteDirection.Up:
                activeUpMusicalNoteList.Remove(noteRectTransform);

                break;

            case NoteDirection.Right:
                activeRightMusicalNoteList.Remove(noteRectTransform);

                break;

            default:

                break;
        }

        allActiveMusicalNoteList.Remove(noteRectTransform);
    }

    public List<RectTransform> GetActiveMusicalNotes(NoteDirection noteDirection)
    {
        switch (noteDirection)
        {
            case NoteDirection.Left:
                return activeLeftMusicalNoteList;

            case NoteDirection.Down:
                return activeDownMusicalNoteList;

            case NoteDirection.Up:
                return activeUpMusicalNoteList;

            case NoteDirection.Right:
                return activeRightMusicalNoteList;

            default:
                return null;
        }
    }

    public List<RectTransform> GetAllActiveMusicalNotes()
    {
        return allActiveMusicalNoteList;
    }
    #endregion
}
