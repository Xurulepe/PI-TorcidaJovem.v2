using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    private int comboCount = 0;
    private MusicalNoteDataSO currentMusicalNoteData;
    [SerializeField] private int maxNotesToSpawn;
    private int deactivatedNotesCount;

    private int maxScore;

    [Header("Musical Notes Settings")]
    [SerializeField] private MusicalNoteDataSO perfectMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO goodMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO coolMusicalNoteData;
    [SerializeField] private MusicalNoteDataSO missMusicalNoteData;

    public int Score => score;
    public int ComboCount => comboCount;
    public MusicalNoteDataSO CurrentMusicalNoteData => currentMusicalNoteData;
    public int MaxNotesToSpawn => maxNotesToSpawn;
    public int DeactivatedNotesCount => deactivatedNotesCount;

    public event Action OnScoreChanged;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

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
            Debug.Log("Fim de game");
            Debug.Log("Score: " + score);
            Debug.Log("Max score: " + PlayerPrefs.GetInt("Max Score"));

            SaveScore();
        }
    }

    private void SaveScore()
    {
        if (score > PlayerPrefs.GetInt("Max Score"))
        {
            PlayerPrefs.SetInt("Max Score", score);

            Debug.Log("New score: " + score);
        }
    }
}
