using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("Icones Question")]
    public int QualQuestion;

    public Image[] Icone;
    public Sprite IconeQuest;
    public Sprite IconeCerto;
    public Sprite IconeErrado;

    [Header("Resultado")]
    public int quantidadePerguntas = 5;
    public int acertos = 0;

    public GameObject TelaVitoria;
    public GameObject TelaDerrota;

    private bool[] Respostas;
    private bool[] Respondidas;

    private void Start()
    {
        Shuffle(_perg);

        // Garante que teremos no máximo 5 perguntas
        quantidadePerguntas = Mathf.Min(5, _perg.Count);

        Respostas = new bool[quantidadePerguntas];
        Respondidas = new bool[quantidadePerguntas];

        _quest = 0;
        acertos = 0;

        // Esconde as telas de resultado
        if (TelaVitoria != null)
            TelaVitoria.SetActive(false);

        if (TelaDerrota != null)
            TelaDerrota.SetActive(false);

        ControleIcone();
        generateQuestion();
    }

    // =========================================================
    // CONTROLE DOS ÍCONES
    // =========================================================

    public void ControleIcone()
    {
        for (int i = 0; i < Icone.Length; i++)
        {
            // Se não existe pergunta para esse ícone
            if (i >= quantidadePerguntas)
            {
                Icone[i].gameObject.SetActive(false);
                continue;
            }

            Icone[i].gameObject.SetActive(true);

            // Ainda não respondeu
            if (!Respondidas[i])
            {
                Icone[i].sprite = IconeQuest;
            }
            // Já respondeu
            else
            {
                if (Respostas[i])
                {
                    Icone[i].sprite = IconeCerto;
                }
                else
                {
                    Icone[i].sprite = IconeErrado;
                }
            }
        }
    }

    // =========================================================
    // RESPOSTA
    // =========================================================

    public void Correct(int value, Button botao)
    {
        if (respondendo)
            return;

        respondendo = true;

        // Verifica a resposta
        bool acertou = _perg[_quest].CheckPerg(value);

        // Salva resposta
        Respostas[_quest] = acertou;
        Respondidas[_quest] = true;

        if (acertou)
        {
            acertos++;
        }

        // Atualiza os ícones
        ControleIcone();

        // Feedback visual
        StartCoroutine(FeedbackResposta(acertou, botao));
    }

    // =========================================================
    // FEEDBACK
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

                for (int i = 0; i < quantidadePiscadas; i++)
                {
                    imagemBotao.color = corFeedback;

                    if (acertou)
                        Personagem.sprite = ImagemCerta;
                    else
                        Personagem.sprite = ImagemErrada;

                    yield return new WaitForSeconds(tempoPiscar);

                    imagemBotao.color = corNormal;
                    Personagem.sprite = ImagemDuvida;

                    yield return new WaitForSeconds(tempoPiscar);
                }

                imagemBotao.color = corFeedback;

                yield return new WaitForSeconds(tempoPiscar);
            }
        }

        // =====================================================
        // VERIFICA SE ACABARAM AS 5 PERGUNTAS
        // =====================================================

        if (_quest >= quantidadePerguntas - 1)
        {
            FinalizarQuiz();
            yield break;
        }

        // Próxima pergunta
        _quest++;

        generateQuestion();

        // Reset dos botões
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
    // FINALIZA O QUIZ
    // =========================================================

    void FinalizarQuiz()
    {
        Debug.Log("QUIZ FINALIZADO!");
        Debug.Log("Acertos: " + acertos + " / " + quantidadePerguntas);

        // Mais acertos do que erros = VITÓRIA
        int erros = quantidadePerguntas - acertos;

        if (acertos > erros)
        {
            Debug.Log("JOGADOR GANHOU!");

            if (TelaVitoria != null)               
                TelaVitoria.SetActive(true);
            
        }
        else
        {
            Debug.Log("JOGADOR PERDEU!");

            if (TelaDerrota != null)
                TelaDerrota.SetActive(true);
        }

        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].enabled = false;
        }

        respondendo = true;
    }


    public void SairGame()
    {
        SceneManager.LoadScene("Cenas_select");
    }

    public void ResetarJogo()
    {
        SceneManager.LoadScene("Quiz");
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