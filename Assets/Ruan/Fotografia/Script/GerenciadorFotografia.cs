using System.Collections;
using UnityEngine;

public class GerenciadorFotografia : MonoBehaviour
{
    public static GerenciadorFotografia instance;

    public GameObject npcSelecionado;


    public Animator animaDiafragma;
    public bool inFoto;

    [Header("controle de missao")]
    public GameObject[] missao;
    public GameObject[] Fotos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }   

    public void ExecutarFoto()
    {
        if (inFoto == false)
        {
            animaDiafragma.SetTrigger("Foto");
            if (npcSelecionado != null)
            {
                if (npcSelecionado.GetComponent<Npcs_Base>().ObjMissao == true)
                {
                    if (missao[npcSelecionado.GetComponent<Npcs_Base>().QualMissao].activeInHierarchy == true)
                    {
                        StartCoroutine(AbrirFoto());
                        //missao[npcSelecionado.GetComponent<Npcs_Base>().QualMissao].GetComponent<Animator>().SetTrigger("Sair");

                        inFoto = true;
                    }
                }
            }
        }        
    }

    public void ExecutarSaidaFoto()
    {
        animaDiafragma.SetTrigger("Foto");
    }

    public IEnumerator AbrirFoto()
    {
        yield return new WaitForSeconds(0.28f);
        Fotos[npcSelecionado.GetComponent<Npcs_Base>().QualMissao].SetActive(true);

    }
}

