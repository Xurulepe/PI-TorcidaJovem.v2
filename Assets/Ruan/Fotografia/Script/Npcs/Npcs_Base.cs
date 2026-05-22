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
    public GameObject go_sprite;
    public SpriteRenderer sp_rendere;
    public Sprite Frente;
    public Sprite Costa;

    [Header("Parede")]
    public LayerMask paredeLayer;
    public float distanciaParede = 0.5f;

    void Start()
    {
        EscolherNovaDirecao();
    }

    void Update()
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
        controleDirecaoSprite();

        if (direcaoAtual.y > 0)
        {
            sp_rendere.sprite = Costa;
        }
        else if (direcaoAtual.y < 0)
        {
            sp_rendere.sprite = Frente;
        }

        _anima.SetBool("Walk", !parado);
    }

    public void controleDirecaoSprite()
    {
        if (direcaoAtual.x < 0 && direcaoAtual.y < 0)
        {
            go_sprite.transform.localScale = new Vector2(-1, 1);
        }
        else if (direcaoAtual.x > 0 && direcaoAtual.y < 0)
        {
            go_sprite.transform.localScale = new Vector2(1, 1);
        }
        else if (direcaoAtual.x < 0 && direcaoAtual.y > 0)
        {
            go_sprite.transform.localScale = new Vector2(-1, 1);
        }
        else if (direcaoAtual.x > 0 && direcaoAtual.y > 0)
        {
            go_sprite.transform.localScale = new Vector2(1, 1);
        }
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