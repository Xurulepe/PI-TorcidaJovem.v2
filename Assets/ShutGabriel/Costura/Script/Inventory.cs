using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory singleton;
    public static InventoryItem carriedItem;
    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] equipmentSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemprefab;
    
    [SerializeField] Item[] items;
    [SerializeField] Button giveItemBtn;

    
    private void Awake()
    {
        singleton = this;
        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }

    private void Update()
    {
        if (carriedItem == null)
        {
            return;
        }
        carriedItem.transform.position = Input.mousePosition;
    }
    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if(item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag)
            {
                return;
            }
            item.activeSlot.setItem(carriedItem);
            if (item.activeSlot.myTag != SlotTag.None )
            {
                EquipEquipment(item.activeSlot.myTag, null);
            }
            carriedItem = null;
            carriedItem.canvasGroup.blocksRaycasts = false;
            item.transform.SetParent(draggablesTransform);
        }
    }
    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag) 
        {
            case SlotTag.Head:
                if (item == null)
                {
                    Debug.Log("RemoveuItem da tag head");
                }
                else
                {
                    Debug.Log("equipou da tag head");
                }
                break;
            case SlotTag.Chest:
                break;
            case SlotTag.Legs:
                break;
            case SlotTag.Feet:
                break;
        }
    }
    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if (_item == null)
        {
            item = PickRandomItem();
        }
        for (int i = 0; i < inventorySlots.Length; i++) 
        {
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemprefab, inventorySlots[i]. transform).Initialize(_item, inventorySlots[i]);
                break;
            }
        }
    }
    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}
