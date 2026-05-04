using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup  { get; private set; }
    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }
    public void Initialize(Item item, InventorySlot parent)
    {
        Debug.Log("=== INIT ===");

        if (item == null)
            Debug.LogError("ITEM NULL");

        if (parent == null)
            Debug.LogError("SLOT NULL");

        if (item != null && item.sprite == null)
            Debug.LogError("SPRITE NULL");

        if (itemIcon == null)
            Debug.LogError("IMAGE NULL");

        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) 
        {
            Inventory.singleton.SetCarriedItem(this);
        }
    }
}
