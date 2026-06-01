using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public List<perguntas> _perg = new List<perguntas>();
    public TextMeshProUGUI[] options;
    public int _quest;
    public int currentQuestion;

    public TMP_Text QuestionTxt;


    private void Start()
    {
        Shuffle(_perg);
        generateQuestion();
    }

    public void correct(int value)
    {
        _perg[_quest].CheckPerg(value);
        if(_quest>=_perg.Count-1)
        {
            _quest = 0;
        }
        else
        {
            _quest++;
            generateQuestion();
        }

    }

    void SetAnswers()
    {
        for (int i=0; i < _perg[_quest]._respostas.Length; i++)
        {

            options[i].text = "" + _perg[_quest]._respostas[i];

        }
    }


    void generateQuestion()
    {
        //currentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = _perg[_quest]._pergunta;
        SetAnswers();

        
    }

    void Shuffle(List<perguntas> quest)
    {
        for (int t = 0; t < quest.Count; t++)
        {
            perguntas tmp = quest[t];
            int r = Random.Range(t, quest.Count);

            quest[t] = quest[r];
            quest[r] = tmp;
        }
    }
}
