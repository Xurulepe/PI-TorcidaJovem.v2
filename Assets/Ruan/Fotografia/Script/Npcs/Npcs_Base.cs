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
    public float distanciaParede = 0.5f;

    [Header("controleMissao")]
    public bool ObjMissao;
    public int QualMissao;

    void Start()
    {
        SelecSprite();
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

            ControleSprite();
            sp_rendere.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);

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

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direcaoAtual,
            distanciaParede,
            paredeLayer
        );

        Debug.DrawRay(transform.position, direcaoAtual * distanciaParede, Color.red);

        if (hit.collider != null)
        {
            // Faz o NPC ir para longe da parede
            direcaoAtual = -direcaoAtual;

            // Novo tempo andando
            timer = Random.Range(tempoAndandoMin, tempoAndandoMax);
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
        if (direcaoAtual == Vector2.zero) return;

        Gizmos.color = Color.red;

        // Linha da visão
        Gizmos.DrawLine(
            transform.position,
            transform.position + (Vector3)(direcaoAtual.normalized * distanciaParede)
        );

        // Bola no final da visão
        Gizmos.DrawSphere(
            transform.position + (Vector3)(direcaoAtual.normalized * distanciaParede),
            0.08f
        );
    }
}