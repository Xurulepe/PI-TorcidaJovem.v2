using DG.Tweening;
using UnityEngine;

public class PanelAnimationn : MonoBehaviour
{
    [SerializeField] private RectTransform painel;

    void Start()
    {
        // Guarda a posição configurada no Inspector
        float posicaoFinal = painel.anchoredPosition.x;

        // Coloca o painel fora da tela à esquerda
        painel.anchoredPosition = new Vector2(
            -painel.rect.width,
            painel.anchoredPosition.y
        );

        // Move até a posição original
        painel.DOAnchorPosX(posicaoFinal, 1f)
            .SetEase(Ease.OutCubic);
    }
}
