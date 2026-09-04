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
        painelTransicao.alpha = 0f;
        vestidoFinal.SetActive(false);
    }

    public void Finalizar()
    {
        painelTransicao.DOKill();
        spriteVestido.DOKill();
        painelTransicao.blocksRaycasts = true;
        painelTransicao.DOFade(1f, duracaoFade)
            .OnComplete(() =>
            {
                vestidoFinal.SetActive(true);
                Color cor = spriteVestido.color;
                cor.a = 0f;
                spriteVestido.color = cor;
                DOVirtual.DelayedCall(tempoEspera, () =>
                {
                    painelTransicao.DOFade(0f, duracaoFade);
                    spriteVestido.DOFade(1f, duracaoFade)
                        .OnComplete(() =>
                        {
                            painelTransicao.blocksRaycasts = false;
                        });
                });
            });
    }
}

