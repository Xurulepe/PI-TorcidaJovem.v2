using UnityEngine;

public class Portas : MonoBehaviour
{
    public Animator anima;
    public GameObject Seta;
    public bool JogoPronto;
    public string nomeCena;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (JogoPronto == true)
        {
            anima.SetBool("Abrir", true);
            Seta.SetActive(true);
            Gerenciador.instance.NomeCena = nomeCena;
            Gerenciador.instance.PodeCarregar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anima.SetBool("Abrir", false);
        Seta.SetActive(false);
        Gerenciador.instance.NomeCena = null;
        Gerenciador.instance.PodeCarregar = false;
    }
}
