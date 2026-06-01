using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Pergunta", menuName = "Pergunta")]


public class perguntas : ScriptableObject
{
    public string _pergunta;
    public string[] _respostas;
    public int _correct;

    public void CheckPerg(int value)
    {
        if(value == _correct)
        {
            Debug.Log("Certa reposta");
        }
        else
        {
            Debug.Log("Errada reposta");
        }
    }

   



}
