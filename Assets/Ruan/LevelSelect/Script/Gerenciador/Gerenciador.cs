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

    [Header("Musica Control")]
    public AudioClip audioClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(audioClip);
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
