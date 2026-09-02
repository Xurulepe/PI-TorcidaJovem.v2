using System.Collections.Generic;
using UnityEngine;
public class BotaoFinalizar : MonoBehaviour
{

    [Header("Peças do vestido")]
    public List<GameObject> pecasDoVestido;

    [Header("Botão Finalizar")]
    public GameObject botaoFinalizar;

    void Start()
    {
        botaoFinalizar.SetActive(false);
    }

    void Update()
    {
        VerificarPecas();
    }

    void VerificarPecas()
    {
        foreach (GameObject peca in pecasDoVestido)
        {
            if (!peca.activeSelf)
            {
                return;
            }
        }
        botaoFinalizar.SetActive(true);
    }
}
