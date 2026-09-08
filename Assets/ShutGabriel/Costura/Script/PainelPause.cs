using UnityEngine;
public class PainelPause : MonoBehaviour
{
    public GameObject painel;
    public void Pausar()
    {
        painel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Despausar()
    {
        painel.SetActive(false);
        Time.timeScale = 1f;
    }
}
