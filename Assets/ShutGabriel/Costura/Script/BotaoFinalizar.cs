using System.Collections.Generic;
using UnityEngine;
public class BotaoFinalizar : MonoBehaviour
{

    [Header("Peças do vestido")]
    public List<GameObject> pecasDoVestido;

    [Header("Botão Finalizar")]
    public GameObject botaoFinalizar;

    [Header("FINAL")]
    [SerializeField] private FINALPAINEl _finalPainel;

    void Start()
    {
        botaoFinalizar.SetActive(false);
    }

    private void Update()
    {
        VerificarPecas();
    }

    public void VerificarPecas()
    {
        if (_finalPainel.vestidoFinal.activeSelf)
        {
            botaoFinalizar.SetActive(false);
            return;
        }

        foreach (GameObject peca in pecasDoVestido)
        {
            if (!peca.activeSelf)
            {
                botaoFinalizar.SetActive(false);
                return;
            }
        }

        botaoFinalizar.SetActive(true);
    }
}

