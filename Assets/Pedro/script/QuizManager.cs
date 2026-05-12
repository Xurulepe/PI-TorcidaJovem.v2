using UnityEngine;
using TMPro;
using DG.Tweening;

public class QuizManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    [Header("Pergunta")]
    [TextArea]
    public string incompleteSentence = "____ is your name?";

    public string correctAnswer = "What";

    void Start()
    {
        questionText.text = incompleteSentence;
        feedbackText.text = "";
    }

    public void CheckAnswer(string selectedAnswer)
{
    if (selectedAnswer == correctAnswer)
    {
        // Substitui o underline
        questionText.text = incompleteSentence.Replace("____", selectedAnswer);

        // Cor normal
        questionText.color = Color.white;

        // Faz piscar por 2 segundos
        questionText
            .DOFade(0f, 0.2f)
            .SetLoops(10, LoopType.Yoyo);

        feedbackText.text = "Correto!";
        feedbackText.color = Color.green;
    }
    else
    {
        feedbackText.text = "Errado!";
        feedbackText.color = Color.red;
    }
}
}