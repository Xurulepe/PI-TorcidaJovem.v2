using UnityEngine;

public class CaixaMissao : MonoBehaviour
{
    public GameObject nextCaixa;

    public void AtivarCaixa()
    {
        if (nextCaixa != null)
        {
            nextCaixa.SetActive(true);
        }
    }

    public void DesativarCaixa()
    {
        transform.gameObject.SetActive(false);
    }
}
