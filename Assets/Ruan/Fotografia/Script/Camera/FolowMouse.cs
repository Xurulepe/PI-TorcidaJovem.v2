using UnityEngine;

public class FolowMouse : MonoBehaviour
{
    public float velocidade = 10f;

    void Update()
    {
        Vector3 destino = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destino.z = 0f;

        transform.position = Vector3.Lerp(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );
    }
}
