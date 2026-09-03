
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Pergunta", menuName = "Pergunta")]
public class perguntas : ScriptableObject
{
    public string _pergunta;

    public string[] _respostas;

    // Índice da resposta correta
    public int _correct;

    public bool CheckPerg(int value)
    {
        if (value == _correct)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

