using MiniGame.TecInformatica;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VirtualHandGrabItem : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    private bool isGrabbingAnyItem = false;
    private DraggableItem draggableItem;
    public LayerMask layerMask;
    public Transform uiElement;

    public void HandleItemGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleItemInteraction(); 
        }
    }

    private void HandleItemInteraction()
    {
        if (isGrabbingAnyItem)
        {
            TryDropItem();
        }
        else
        {
            TryGrabItem();
        }
    }


    private void TryDropItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(transform.position);

        Debug.Log(ray.direction);
    }

    private void TryGrabItem()
    {
        Vector3 worldPosition = Camera.main.WorldToScreenPoint(transform.position);
        uiElement.position = worldPosition;

        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = worldPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit UI Image: " + result.gameObject.name);

            if (result.gameObject.TryGetComponent(out DraggableItem draggableItem))
            {
                isGrabbingAnyItem = true;
                Debug.Log("Grab " +  draggableItem.gameObject.name);

                draggableItem.ParentAfterDrag = uiElement;
                draggableItem.transform.SetParent(uiElement);

                this.draggableItem = draggableItem;
            }
        }
    }
}
