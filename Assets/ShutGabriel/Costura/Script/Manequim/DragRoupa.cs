using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TipoRoupa
{
    CamisaCostas,
    CamisaFrente,
    Calcinha,
    Saia
}

public class DragRoupa : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public TipoRoupa tipo;

    private Image imagem;
    private Vector3 posicaoInicial;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        imagem = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public Sprite GetSprite()
    {
        return imagem.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        posicaoInicial = transform.position;

        transform.SetAsLastSibling();

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = posicaoInicial;
        canvasGroup.blocksRaycasts = true;
    }
}