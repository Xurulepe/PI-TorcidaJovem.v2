using UnityEngine;
using UnityEngine.EventSystems;

public class DragManequimdrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    [SerializeField] private GameObject roupaManequim;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Arrastar");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("ArrasTANDivos");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Soltivo");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDowned");
    }
    public void Vestir()
    {
        gameObject.SetActive(false);
        roupaManequim.SetActive(true);
    }
}
