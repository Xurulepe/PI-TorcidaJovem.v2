using System.Collections;
using UnityEngine;

public class ControleUi : MonoBehaviour
{
    public GameObject BtnVoltar;


    public void Update()
    {       
        BtnVoltar.SetActive(GerenciadorFotografia.instance.inFoto);        
    }
    public void BtnFecharFoto()
    {
        StartCoroutine(FecharFoto());
    }

    public IEnumerator FecharFoto()
    {
        GerenciadorFotografia.instance.ExecutarSaidaFoto();

        yield return new WaitForSeconds(0.28f);

        GerenciadorFotografia.instance.Fotos[GerenciadorFotografia.instance.npcSelecionado.GetComponent<Npcs_Base>().QualMissao].SetActive(false);
        
        yield return new WaitForSeconds(0.28f);
        GerenciadorFotografia.instance.missao[GerenciadorFotografia.instance.npcSelecionado.GetComponent<Npcs_Base>().QualMissao].GetComponent<Animator>().SetTrigger("Sair");
                
        GerenciadorFotografia.instance.inFoto = false;

        if (GerenciadorFotografia.instance.FotosTirada == 3)
        {
            GerenciadorFotografia.instance.UltimoAberto = true;
        }
    }
}
