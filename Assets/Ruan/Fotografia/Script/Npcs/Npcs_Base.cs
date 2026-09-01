using UnityEngine;

public class Npcs_Base : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 2f;

    [Header("Tempo Andando")]
    public float tempoAndandoMin = 1f;
    public float tempoAndandoMax = 4f;

    [Header("Tempo Parado")]
    public float tempoParadoMin = 1f;
    public float tempoParadoMax = 3f;

    [Header("Direção Atual")]
    public Vector2 direcaoAtual;

    [Header("Estado")]
    public bool parado;

    private float timer;

    [Header("Controle de animação")]
    public Animator _anima;

    [Header("Controle de sprite")]
    public SpriteRenderer sp_rendere;
    public Sprite[] presonas;

    [Header("Parede")]
    public LayerMask paredeLayer;
    public float raioDeteccao = 0.25f;
    public Transform PontoRaycast;

    [Header("Detecção")]
    public float tempoSemDetectar = 0.3f;

    private float cooldownDeteccao;

    [Header("controleMissao")]
    public bool ObjMissao;
    public int QualMissao;

    void Start()
    {
        if (ObjMissao == false)
        {
            SelecSprite();
        }
    }

    void Update()
    {
        if (GerenciadorFotografia.instance.inFoto == false)
        {
            timer -= Time.deltaTime;

            DetectarParede();

            if (timer <= 0)
            {
                if (parado)
                {
                    EscolherNovaDirecao();
                }
                else
                {
                    FicarParado();
                }
            }

            if (!parado)
            {
                Mover();
            }

            if (cooldownDeteccao > 0)
                cooldownDeteccao -= Time.deltaTime;

            DetectarParede();

            ControleSprite();

        }

    }

    #region Controle Movimento

    void EscolherNovaDirecao()
    {
        parado = false;

        // Tempo aleatório andando
        timer = Random.Range(tempoAndandoMin, tempoAndandoMax);

        // Direção aleatória
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        direcaoAtual = new Vector2(x, y).normalized;
    }

    void FicarParado()
    {
        parado = true;

        // Tempo aleatório parado
        timer = Random.Range(tempoParadoMin, tempoParadoMax);

        direcaoAtual = Vector2.zero;
    }

    void Mover()
    {
        // Movimento isométrico
        Vector3 movimento = new Vector3(
            direcaoAtual.x - direcaoAtual.y,
            (direcaoAtual.x + direcaoAtual.y) / 2,
            0
        );

        transform.position += movimento * velocidade * Time.deltaTime;
    }

    void DetectarParede()
    {
        if (parado) return;
        if (cooldownDeteccao > 0) return;

        Collider2D hit = Physics2D.OverlapCircle(
            PontoRaycast.position,
            raioDeteccao,
            paredeLayer
        );

        if (hit != null)
        {
            // Direção para longe da parede
            Vector2 novaDirecao = ((Vector2)transform.position - hit.ClosestPoint(transform.position)).normalized;

            // Se por algum motivo a direção ficar zerada
            if (novaDirecao == Vector2.zero)
                novaDirecao = -direcaoAtual;

            direcaoAtual = novaDirecao;

            // Continua andando por mais um tempo
            timer = Random.Range(tempoAndandoMin, tempoAndandoMax);

            // Ignora novas detecções por alguns instantes
            cooldownDeteccao = tempoSemDetectar;
        }
    }

    #endregion

    #region Controle Sprite

    public void ControleSprite()
    {
        _anima.SetBool("Walk", !parado);
        if (direcaoAtual.x > 0)
        {
            sp_rendere.flipX = false;
        }
        else if(direcaoAtual.x < 0)
        {
            sp_rendere.flipX = true;
        }
    }

    public void SelecSprite()
    {
        sp_rendere.sprite = presonas[Random.Range(0, presonas.Length)];
    }

    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PontoRaycast.position, raioDeteccao);
    }
}