using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGame.TecInformatica
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [Header("Inventory Slots Settings")]
        [SerializeField] private bool requiresItem;
        [SerializeField] private ItemType requiredItem;

        private bool canDropItem = true;

        public ItemType RequiredItem => requiredItem;

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

                TryReplace(currentObjectInSlot, draggableItem);
            }

            if (!canDropItem)
            {
                return;
            }

            draggableItem.ParentAfterDrag = transform;
            draggableItem.SetInventorySlot(this);
        }

        private void TryReplace(Transform currentObjectInSlot, DraggableItem draggableItem)
        {
            if (currentObjectInSlot.TryGetComponent(out DraggableItem currentItemInSlot))
            {
                InventorySlot draggableItemSlot = draggableItem.ParentAfterDrag.GetComponent<InventorySlot>();

                if (draggableItemSlot.requiredItem == ItemType.None || draggableItemSlot.requiredItem == currentItemInSlot.ItemType)
                {
                    currentObjectInSlot.SetParent(draggableItem.ParentAfterDrag);

                    canDropItem = true;
                }
                else
                {
                    canDropItem = false;
                }
            }
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
        PSU,
        Cooler
    }
}
