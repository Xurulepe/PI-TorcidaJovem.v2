using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private ItemType requiredItem;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem.ItemType != requiredItem)
        {
            return;
        }

        if (transform.childCount != 0)
        {
            Transform currentObjectInSlot = transform.GetChild(0);
                        
            currentObjectInSlot.SetParent(draggableItem.ParentAfterDrag);
        }

        draggableItem.ParentAfterDrag = transform;
    }
}

public enum ItemType
{
    None,
    Motherboard,
    CPU,
    RAM,
    GPU,
    HD,
    PSU
}
