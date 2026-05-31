using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGame.TecInformatica
{
    public class TableComponentSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private List<InventorySlot> inventorySlotList = new List<InventorySlot>();

        private Dictionary<ItemType, InventorySlot> inventorySlotDict = new Dictionary<ItemType, InventorySlot>();

        private void Awake()
        {
            InitDictonary();
        }

        private void InitDictonary()
        {
            inventorySlotDict.Clear();

            foreach (var slot in inventorySlotList)
            {
                inventorySlotDict.Add(slot.RequiredItem, slot);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            inventorySlotDict.TryGetValue(draggableItem.ItemType, out InventorySlot inventorySlotItem);

            draggableItem.SetParentAfterDrag(inventorySlotItem.transform);
            draggableItem.SetInventorySlot(inventorySlotItem);
        }
    }
}
