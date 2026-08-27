using UnityEngine;
using UnityEngine.SceneManagement;

public class Gerenciador : MonoBehaviour
{
   public static Gerenciador instance;

    public string NomeCena;
    public bool PodeCarregar;
    public Animator fadeAnima;

    [Header("scriptPlayer")]
    public Player playerSc;

    [Header("Setas")]
    public GameObject setas;
    public GameObject SetaFrente;
    public GameObject SetaTras;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }

    public void GoNextDoor()
    {
        playerSc.pontoDestino ++;
        playerSc.moverParaPonto = true;
    }

    public void GoFowarDoor()
    {
        playerSc.pontoDestino--;
        playerSc.moverParaPonto = true;
    }

    public void EnterPorta()
    {
        playerSc.EnterLevel();
    }

    public void LoadCenaMinigame()
    {
        SceneManager.LoadScene(NomeCena);
    }


}
