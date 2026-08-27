using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PiscarIMG : MonoBehaviour
{
    public Image imagem;
    public float intervalo = 0.5f;

    Color laranja = new Color(1f, 0.5f, 0f);
    Color azul = Color.blue;

    void Start()
    {
        StartCoroutine(Piscar());
    }

    IEnumerator Piscar()
    {
        while (true)
        {
            imagem.color = laranja;
            yield return new WaitForSeconds(intervalo);

            imagem.color = azul;
            yield return new WaitForSeconds(intervalo);
        }
    }
}
