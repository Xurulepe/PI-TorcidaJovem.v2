using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorFotografia : MonoBehaviour
{
    public static GerenciadorFotografia instance;

    public GameObject npcSelecionado;


    public Animator animaDiafragma;
    public bool inFoto;

    [Header("controle de missao")]
    public GameObject[] missao;
    public GameObject[] Fotos;

    public int FotosTirada;

    [Header("TelaFinal")]
    public GameObject telaFinal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (FotosTirada >= 3 && inFoto == false)
        {
            telaFinal.SetActive(true);
            inFoto = true;
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
        FotosTirada++;

    }

    public void sairSelecFase()
    {
        SceneManager.LoadScene("Cenas_select");
    }
}

