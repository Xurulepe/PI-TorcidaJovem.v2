using UnityEngine;

public class Player : MonoBehaviour
{
    InputPlayer _Controls;

    [Header("componentes")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _anima;

    [Header("controle de movimento")]
    [SerializeField] bool podeMover;
    [SerializeField] bool _movendo;
    [SerializeField] float _veloMove;
    [SerializeField] Vector2 _directios;

    [Header("Movimento automatico")]
    [SerializeField] public bool moverParaPonto;
    [SerializeField] public int pontoDestino;
    [SerializeField] float distanciaParaParar = 0.05f;

    [Header("pontos Player")]
    public GameObject[] pontosParada;


    void Start()
    {
        podeMover = true;
        pontoDestino = 0;
        moverParaPonto = true;

    }

    void Update()
    {
        ControleAnima();
        Limites();
    }

    private void FixedUpdate()
    {
        if (moverParaPonto)
        {
            MoverParaPonto();
        }
    }

    #region controleMovimento

    /*
    public void movePLayer(Vector2 Direcao)
    {
        if (Direcao.magnitude != 0)
        {
            _movendo = true;
        }
        else
        {
            _movendo = false;
        }

        if (Direcao.x > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (Direcao.x < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        _rb.linearVelocity = new Vector2(
            _veloMove * Direcao.x,
            _rb.linearVelocityY
        );
    }
    */
    private void MoverParaPonto()
    {
        // Verifica se o índice existe
        if (pontosParada == null ||
            pontosParada.Length == 0 ||
            pontoDestino < 0 ||
            pontoDestino >= pontosParada.Length)
        {
            moverParaPonto = false;
            _movendo = false;
            return;
        }

        GameObject ponto = pontosParada[pontoDestino];

        // Verifica se o ponto existe
        if (ponto == null)
        {
            moverParaPonto = false;
            _movendo = false;
            return;
        }

        Vector2 destino = ponto.transform.position;

        // Movimento até o ponto
        Vector2 novaPosicao = Vector2.MoveTowards(
            _rb.position,
            destino,
            _veloMove * Time.deltaTime
        );

        _rb.MovePosition(novaPosicao);

        _movendo = true;

        // Vira o personagem para a direção do movimento
        float diferencaX = destino.x - transform.position.x;

        if (diferencaX > 0.01f)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (diferencaX < -0.01f)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        // Chegou ao destino
        if (Vector2.Distance(_rb.position, destino) <= distanciaParaParar)
        {
            _rb.position = destino;

            _rb.linearVelocity = Vector2.zero;

            _movendo = false;

            moverParaPonto = false;
        }

            Gerenciador.instance.setas.SetActive(!moverParaPonto);

    }

    public void EnterLevel()
    {
        if (Gerenciador.instance.NomeCena != null &&
            Gerenciador.instance.PodeCarregar == true)
        {
            podeMover = false;
            Gerenciador.instance.fadeAnima.SetTrigger("Sair");
        }
    }

    public void Limites()
    {
        if (pontoDestino < 0)
        {
            pontoDestino = 0;
        }
        if (pontoDestino > pontosParada.Length-1)
        {
            pontoDestino = pontosParada.Length-1;
        }

        if (pontoDestino > 0)
        {
            Gerenciador.instance.SetaTras.SetActive(true);
        }
        else
        {
            Gerenciador.instance.SetaTras.SetActive(false);

        }

        if (pontoDestino < pontosParada.Length - 1)
        {
            Gerenciador.instance.SetaFrente.SetActive(true);
        }
        else
        {
            Gerenciador.instance.SetaFrente.SetActive(false);

        }

       
    }

    #endregion

    #region controleAnima

    public void ControleAnima()
    {
        _anima.SetBool("Walk", _movendo);
    }

    #endregion
    
}
