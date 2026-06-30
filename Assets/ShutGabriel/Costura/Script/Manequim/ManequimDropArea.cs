using UnityEngine;
using UnityEngine.EventSystems;

public class ManequimDropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DragManequimdrop item = eventData.pointerDrag.GetComponent<DragManequimdrop>();

        if (item != null)
        {
            item.ConfirmarDrop();
            item.Vestir();
        }
    }


}