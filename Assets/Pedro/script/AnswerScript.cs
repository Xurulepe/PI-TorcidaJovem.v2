
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public QuizManager quizManager;

    // 0 = primeira resposta
    // 1 = segunda resposta
    // 2 = terceira resposta
    // 3 = quarta resposta
    public int valorResposta;

    private Button botao;

    private void Awake()
    {
        botao = GetComponent<Button>();
    }

    public void Answer()
    {
        if (quizManager == null)
        {
            Debug.LogError("QuizManager não foi configurado no AnswerScript!");
            return;
        }

        quizManager.Correct(valorResposta, botao);
    }
}

