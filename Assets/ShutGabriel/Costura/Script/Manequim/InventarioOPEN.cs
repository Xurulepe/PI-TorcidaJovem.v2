using UnityEngine;

public class InventarioOPEN : MonoBehaviour
{
    public GameObject inventario;

    private bool jogadorTemInventario = false;

    public void BotaoRecorte()
    {
        jogadorTemInventario = true;
        inventario.SetActive(true);
    }

    public void EntrouPrincipal()
    {
        inventario.SetActive(false);
    }

    public void EntrouManequim()
    {
        if (jogadorTemInventario)
            inventario.SetActive(true);
    }
}
