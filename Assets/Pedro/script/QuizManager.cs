
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<perguntas> _perg = new List<perguntas>();

    public TextMeshProUGUI[] options;
    public Button[] btns;

    public int _quest;
    public int currentQuestion;

    public TMP_Text QuestionTxt;

    [Header("Feedback")]
    public Color corNormal = Color.white;
    public Color corCerta = Color.green;
    public Color corErrada = Color.red;

    [Header("Tempo do Feedback")]
    public float tempoPiscar = 0.15f;
    public int quantidadePiscadas = 2;

    private bool respondendo = false;

    [Header("Personagem")]
    public Image Personagem;
    public Sprite ImagemDuvida;
    public Sprite ImagemCerta;
    public Sprite ImagemErrada;

    private void Start()
    {
        Shuffle(_perg);
        generateQuestion();
    }

    // =========================================================
    // RESPOSTA
    // =========================================================

    public void Correct(int value, Button botao)
    {
        // Impede clicar várias vezes enquanto está dando feedback
        if (respondendo)
            return;

        respondendo = true;

        // Verifica diretamente se a resposta está correta
        bool acertou = _perg[_quest].CheckPerg(value);

        // Começa o feedback
        StartCoroutine(FeedbackResposta(acertou, botao));
    }

    // =========================================================
    // FEEDBACK VERDE / VERMELHO
    // =========================================================

    IEnumerator FeedbackResposta(bool acertou, Button botao)
    {
        if (botao != null)
        {
            Image imagemBotao = botao.GetComponent<Image>();

            if (imagemBotao != null)
            {
                Color corFeedback;

                if (acertou)
                    corFeedback = corCerta;
                else
                    corFeedback = corErrada;

               

                // Pisca
                for (int i = 0; i < quantidadePiscadas; i++)
                {
                    imagemBotao.color = corFeedback;

                    if (acertou == true)
                    {
                        Personagem.sprite = ImagemCerta;
                    }
                    else
                    {
                        Personagem.sprite = ImagemErrada;
                    }

                    yield return new WaitForSeconds(tempoPiscar);

                    imagemBotao.color = corNormal;
                    Personagem.sprite = ImagemDuvida;

                    yield return new WaitForSeconds(tempoPiscar);
                }

                // Deixa a cor por mais um instante
                imagemBotao.color = corFeedback;

                yield return new WaitForSeconds(tempoPiscar);
            }
        }

        // =====================================================
        // PRÓXIMA PERGUNTA
        // =====================================================

        if (_quest >= _perg.Count - 1)
        {
            _quest = 0;
        }
        else
        {
            _quest++;
        }

        generateQuestion();

        // =====================================================
        // RESET DOS BOTÕES
        // =====================================================

        foreach (Button btn in btns)
        {
            if (btn == null)
                continue;

            Image img = btn.GetComponent<Image>();

            if (img != null)
                img.color = corNormal;
        }

        respondendo = false;
    }

    // =========================================================
    // CONFIGURA AS RESPOSTAS
    // =========================================================

    void SetAnswers()
    {
        for (int i = 0; i < _perg[_quest]._respostas.Length; i++)
        {
            if (i < options.Length)
            {
                options[i].text = _perg[_quest]._respostas[i];
            }
        }
    }

    // =========================================================
    // GERA PERGUNTA
    // =========================================================

    void generateQuestion()
    {
        if (_perg.Count == 0)
        {
            Debug.LogError("A lista de perguntas está vazia!");
            return;
        }

        QuestionTxt.text = _perg[_quest]._pergunta;

        SetAnswers();
    }

    // =========================================================
    // EMBARALHA PERGUNTAS
    // =========================================================

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
