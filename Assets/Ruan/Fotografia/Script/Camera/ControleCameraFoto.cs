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

    void Update()
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
            print(collision.gameObject.name); 
        }
    }
}