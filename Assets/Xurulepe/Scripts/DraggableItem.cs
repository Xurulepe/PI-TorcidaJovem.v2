using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Draggable Item Settings")]
    [SerializeField] private Image image;

    private Transform parentAfterDrag;

    public Transform ParentAfterDrag
    {
        get { return parentAfterDrag; }
        set 
        { 
            if (value != null) 
                parentAfterDrag = value; 
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);

        image.raycastTarget = true;
    }
}
