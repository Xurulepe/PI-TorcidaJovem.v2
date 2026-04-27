using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [Header("Inventory Slots Settings")]
    [SerializeField] private bool requiresItem;
    [SerializeField] private ItemType requiredItem;

    public event Action OnWrongItemPlaced;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (requiresItem && draggableItem.ItemType != requiredItem)
        {
            OnWrongItemPlaced?.Invoke();

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
