using UnityEngine;

public class Player : MonoBehaviour
{
    InputPlayer _Controls;

    [Header("componentes")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _anima;

    [Header("controle de movimento")]
    [SerializeField] bool _movendo;
    [SerializeField] float _veloMove;
    [SerializeField] Vector2 _directios;


    private void Awake()
    {
        controles();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControleAnima();
    }

    private void FixedUpdate()
    {
        movePLayer(_directios);
    }

    #region controleMovimento
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
        else if(Direcao.x < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        _rb.linearVelocity = new Vector2(_veloMove * Direcao.x * Time.deltaTime, _rb.linearVelocityY);
    }

    #endregion

    #region controleAnima
    public void ControleAnima()
    {
        _anima.SetBool("Walk", _movendo);
    }
    #endregion

    #region input

    public void controles()
    {
        _Controls = new InputPlayer();
        _Controls.Ruan.Move.performed += ctx => _directios = ctx.ReadValue<Vector2>();
        _Controls.Ruan.Move.canceled += ctx => _directios = Vector2.zero;

    }

    public void OnEnable()
    {
        _Controls.Ruan.Enable();
    }

    public void OnDisable()
    {
        _Controls.Ruan.Disable();
    }

    #endregion
}
