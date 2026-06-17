using UnityEngine;
using UnityEngine.InputSystem;

public class ControleCameraFoto : MonoBehaviour
{
    [Header("Controle")]
    public float velocidadeControle = 10f;

    [Header("Mouse")]
    public float tempoSuavizacao = 0.1f;

    private Vector2 movimentoControle;
    private bool usandoControle;

    private Vector3 velocidadeAtual;

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
            if (GerenciadorFotografia.instance.inFoto == true) return;

            GerenciadorFotografia.instance.ExecutarFoto();        
        }
    }

    void Update()
    {
        if (GerenciadorFotografia.instance.inFoto == false)
        {
            ControleCamera();      
        }
        
    }

    public void ControleCamera()
    {
        // Detecta movimento do mouse
        if (Mouse.current != null &&
            Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
        {
            usandoControle = false;
        }

        if (usandoControle)
        {
            transform.position += new Vector3(
                movimentoControle.x,
                movimentoControle.y,
                0f
            ) * velocidadeControle * Time.deltaTime;
        }
        else if (Mouse.current != null)
        {
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
            GerenciadorFotografia.instance.npcSelecionado = collision.gameObject;
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