using UnityEngine;
using DG.Tweening;
public class FINALPAINEl : MonoBehaviour
{
    [Header("Painel Preto")]
    [SerializeField] private CanvasGroup painelTransicao;

    [Header("Vestido Final")]
    [SerializeField] private GameObject vestidoFinal;
    [SerializeField] private SpriteRenderer spriteVestido;

    [Header("Configurações")]
    [SerializeField] private float duracaoFade = 1f;
    [SerializeField] private float tempoEspera = 2f;

    private void Start()
    {
        // Painel começa invisível
        painelTransicao.alpha = 0f;

        // Vestido começa desativado
        vestidoFinal.SetActive(false);
    }

    public void Finalizar()
    {
        painelTransicao.DOKill();
        spriteVestido.DOKill();

        // Bloqueia cliques durante a transição
        painelTransicao.blocksRaycasts = true;

        // Tela fica preta
        painelTransicao.DOFade(1f, duracaoFade)
            .OnComplete(() =>
            {
                // Ativa o vestido enquanto a tela está preta
                vestidoFinal.SetActive(true);

                // Deixa o vestido inicialmente invisível
                Color cor = spriteVestido.color;
                cor.a = 0f;
                spriteVestido.color = cor;

                // Espera
                DOVirtual.DelayedCall(tempoEspera, () =>
                {
                    // Painel preto desaparece
                    painelTransicao.DOFade(0f, duracaoFade);

                    // Vestido aparece gradualmente ⭐
                    spriteVestido.DOFade(1f, duracaoFade)
                        .OnComplete(() =>
                        {
                            painelTransicao.blocksRaycasts = false;
                        });
                });
            });
    }
}

