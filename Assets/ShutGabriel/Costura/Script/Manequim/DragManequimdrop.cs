using UnityEngine;
using UnityEngine.EventSystems;

public class DragManequimdrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    [SerializeField] private GameObject roupaManequim;
    private Vector2 posicaoInicial;
    private bool soltouNoManequim = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Arrastar");
        posicaoInicial = rectTransform.anchoredPosition;
        soltouNoManequim = false;

    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("ArrasTANDivos");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Soltivo");
        if (!soltouNoManequim)
        {
            rectTransform.anchoredPosition = posicaoInicial;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDowned");
    }
    public void ConfirmarDrop()
    {
        soltouNoManequim = true;
    }
    public void Vestir()
    {
        gameObject.SetActive(false);
        roupaManequim.SetActive(true);
    }
}
