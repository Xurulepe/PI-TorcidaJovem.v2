using UnityEngine;
using UnityEngine.SceneManagement;

public class Gerenciador : MonoBehaviour
{
   public static Gerenciador instance;

    public string NomeCena;
    public bool PodeCarregar;
    public Animator fadeAnima;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadCenaMinigame()
    {
        SceneManager.LoadScene(NomeCena);
    }
}
