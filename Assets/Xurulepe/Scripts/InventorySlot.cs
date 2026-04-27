using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (transform.childCount != 0)
        {
            Transform currentObjectInSlot = transform.GetChild(0);
                        
            currentObjectInSlot.SetParent(draggableItem.ParentAfterDrag);
        }

        draggableItem.ParentAfterDrag = transform;
    }
}
