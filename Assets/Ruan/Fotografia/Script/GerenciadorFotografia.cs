using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorFotografia : MonoBehaviour
{
    public static GerenciadorFotografia instance;

    public GameObject npcSelecionado;


    public Animator animaDiafragma;
    public bool inFoto;
    public bool UltimoAberto;

    [Header("controle de missao")]
    public GameObject[] missao;
    public GameObject[] Fotos;

    public int FotosTirada;

    [Header("TelaFinal")]
    public GameObject telaFinal;
    public GameObject BtnVoltar;

    [Header("Sound control")]
    public AudioClip musicaJogo;
    public AudioClip somFoto;
    public AudioClip somImpreFoto;
    public AudioClip somFinalJogo;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(musicaJogo);
    }

    private void Update()
    {
        if (FotosTirada == 3 && UltimoAberto == true)
        {
            BtnVoltar.SetActive(false);
        }

        if (FotosTirada >= 3 && inFoto == false)
        {
            telaFinal.SetActive(true);
            AudioManager.Instance.PlaySFX(somFinalJogo);
            inFoto = true;
        }
    }
    public void ExecutarFoto()
    {
        if (inFoto == false)
        {
            animaDiafragma.SetTrigger("Foto");
            AudioManager.Instance.PlaySFX(somFoto);

            if (npcSelecionado != null)
            {
                if (npcSelecionado.GetComponent<Npcs_Base>().ObjMissao == true)
                {
                    if (missao[npcSelecionado.GetComponent<Npcs_Base>().QualMissao].activeInHierarchy == true)
                    {
                        StartCoroutine(AbrirFoto());
                        AudioManager.Instance.PlaySFX(somImpreFoto);

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

