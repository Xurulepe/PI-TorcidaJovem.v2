using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private int maxNotesToSpawn;

    private MusicalNoteDataSO currentMusicalNoteData;
    private int comboCount = 0;
    private int deactivatedNotesCount;
    private bool isGameRunning = false;
    private int maxScore;

    [Header("Musical Notes Settings")]
    [SerializeField] private MusicalNoteDataSO perfectMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO goodMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO coolMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO missMusicalNoteData;

    [Header("Active Musical Notes")]
    public List<RectTransform> activeLeftMusicalNoteList = new List<RectTransform>();
    public List<RectTransform> activeDownMusicalNoteList = new List<RectTransform>();
    public List<RectTransform> activeUpMusicalNoteList = new List<RectTransform>();
    public List<RectTransform> activeRightMusicalNoteList = new List<RectTransform>();


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

        SetScore(score + currentMusicalNoteData.scoreValue);

        comboCount = 0;
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

            OnGameComplete?.Invoke();
        }
    }

    #region MUSICAL NOTE CONTROLLER
    public void AddActiveNote(RectTransform noteRectTransform, NoteDirection noteDirection)
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
    }

    public void RemoveActiveNote(RectTransform noteRectTransform, NoteDirection noteDirection)
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
    }

    public List<RectTransform> GetActiveNotesList(NoteDirection noteDirection)
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
    #endregion
}
