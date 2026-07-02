using UnityEngine;

public class Portas : MonoBehaviour
{
    public Animator anima;
    public GameObject Seta;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anima.SetBool("Abrir", true);
        Seta.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anima.SetBool("Abrir", false);
        Seta.SetActive(false);

    }
}
