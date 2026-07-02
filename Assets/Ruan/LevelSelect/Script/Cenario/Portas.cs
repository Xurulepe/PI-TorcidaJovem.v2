using UnityEngine;

public class Portas : MonoBehaviour
{
    public Animator anima;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anima.SetBool("Abrir", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anima.SetBool("Abrir", false);
    }
}
