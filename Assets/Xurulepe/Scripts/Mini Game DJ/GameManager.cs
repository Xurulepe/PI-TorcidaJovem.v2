using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    private string accuracyInfo = "";
    private int comboCount = 0;

    [Header("Score Settings")]
    [SerializeField] private int scorePerNote = 10;
    [SerializeField] private int scorePerPerfectNote = 20;
    [SerializeField] private float perfectNoteTimingWindow = 0.2f;
    [SerializeField] private float goodNoteTimingWindow = 0.5f;

    public int Score
    {
        get { return score; }
        set
        {
            if (value < 0)
            {
                score = 0;
            }
            else
            {
                score = value;
            }
        }
    }

    public string AccuracyInfo => accuracyInfo;
    public int ComboCount => comboCount;

    public event Action OnScoreChanged;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void CalculateScore(float distanceToNote)
    {
        if (distanceToNote <= perfectNoteTimingWindow)
        {
            Score += scorePerPerfectNote;
            accuracyInfo = "Perfect!";
        }
        else if (distanceToNote <= goodNoteTimingWindow)
        {
            Score += scorePerNote;
            accuracyInfo = "Good!";
        }
        else
        {
            accuracyInfo = "Hit!";
        }

        comboCount++;

        OnScoreChanged?.Invoke();
    }

    public void ReduceScore()
    {
        Score -= scorePerNote;
        accuracyInfo = "Miss!";
        comboCount = 0;

        OnScoreChanged?.Invoke();
    }
}
