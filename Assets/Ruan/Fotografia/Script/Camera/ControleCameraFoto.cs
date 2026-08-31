using UnityEngine;
using UnityEngine.InputSystem;

public class ControleCameraFoto : MonoBehaviour
{
    [Header("Controle")]
    public float velocidadeControle = 10f;

    [Header("Mouse / Touch")]
    public float tempoSuavizacao = 0.1f;

    [Header("Double Tap")]
    public float tempoMaximoDoubleTap = 0.3f;

    private Vector2 movimentoControle;
    private bool usandoControle;

    private Vector3 velocidadeAtual;

    private float tempoUltimoToque = -1f;

    public void OnMove(InputAction.CallbackContext context)
    {
        movimentoControle = context.ReadValue<Vector2>();

        if (movimentoControle.sqrMagnitude > 0.01f)
            usandoControle = true;
    }

    public void OnFoto(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TirarFoto();
        }
    }

    void Update()
    {
        if (GerenciadorFotografia.instance.inFoto)
            return;

        ControleCamera();
        VerificarDoubleTap();
    }

    private void VerificarDoubleTap()
    {
        if (Touchscreen.current == null)
            return;

        var toque = Touchscreen.current.primaryTouch;

        // Detecta quando o dedo acabou de tocar na tela
        if (toque.press.wasPressedThisFrame)
        {
            float tempoAtual = Time.time;

            // Segundo toque dentro do tempo permitido
            if (tempoUltimoToque >= 0 &&
                tempoAtual - tempoUltimoToque <= tempoMaximoDoubleTap)
            {
                TirarFoto();

                // Reseta para não contar um terceiro toque
                tempoUltimoToque = -1f;
            }
            else
            {
                // Primeiro toque
                tempoUltimoToque = tempoAtual;
            }
        }
    }

    private void TirarFoto()
    {
        if (GerenciadorFotografia.instance.inFoto)
            return;

        GerenciadorFotografia.instance.ExecutarFoto();
    }

    public void ControleCamera()
    {
        // =========================
        // CONTROLE
        // =========================

        if (usandoControle)
        {
            transform.position += new Vector3(
                movimentoControle.x,
                movimentoControle.y,
                0f
            ) * velocidadeControle * Time.deltaTime;
        }

        // =========================
        // TOUCH
        // =========================

        else if (Touchscreen.current != null &&
                 Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 posicaoTouch =
                Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 posTouch =
                Camera.main.ScreenToWorldPoint(posicaoTouch);

            posTouch.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                posTouch,
                ref velocidadeAtual,
                tempoSuavizacao
            );
        }

        // =========================
        // MOUSE
        // =========================

        else if (Mouse.current != null)
        {
            if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
            {
                usandoControle = false;
            }

            Vector3 posMouse = Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

            posMouse.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                posMouse,
                ref velocidadeAtual,
                tempoSuavizacao
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pequenino"))
        {
            GerenciadorFotografia.instance.npcSelecionado =
                collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pequenino"))
        {
            GerenciadorFotografia.instance.npcSelecionado = null;
        }
    }
}