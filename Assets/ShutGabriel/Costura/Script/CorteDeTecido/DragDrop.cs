using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,
    IPointerDownHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IDragHandler
{
    [Header("References")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform dropArea;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 startPosition;

    public static List<DragDrop> allItems =
        new List<DragDrop>();
    CosturaController costuraController;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        costuraController = Camera.main.GetComponent<CosturaController>();
        costuraController.OBJFora.Add(this);
        costuraController.objFisicos.Add(this);
        startPosition = rectTransform.anchoredPosition;

        allItems.Add(this);

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        bool outside = !IsInsideArea();
        bool collision = HasCollision();

        if (outside || collision)
        {
            Debug.Log("RESET");

            ResetAllItems();
        }
        else
        {
            Debug.Log("Na area");
            costuraController.OBJDentro.Add(this);
            costuraController.OBJFora.Remove(this);
        }
    }

    bool IsInsideArea()
    {
        Vector3[] areaCorners = new Vector3[4];
        dropArea.GetWorldCorners(areaCorners);
       

        Vector3[] itemCorners = new Vector3[4];
        rectTransform.GetWorldCorners(itemCorners);

        foreach (Vector3 corner in itemCorners)
        {
            if (corner.x < areaCorners[0].x ||
                corner.x > areaCorners[2].x ||
                corner.y < areaCorners[0].y ||
                corner.y > areaCorners[2].y)
            {
                return false;
            }
        }

        return true;
        
    }

    bool HasCollision()
    {
        Rect myRect = GetWorldRect(rectTransform);

        foreach (DragDrop item in allItems)
        {
            if (item == this)
                continue;

            Rect otherRect =
                GetWorldRect(item.rectTransform);

            if (myRect.Overlaps(otherRect))
            {
                return true;
            }
        }

        return false;
    }

    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        return new Rect(
            corners[0].x,
            corners[0].y,
            corners[2].x - corners[0].x,
            corners[2].y - corners[0].y
        );
    }

    void ResetAllItems()
    {
        costuraController.OBJDentro.Clear();
        costuraController.OBJFora.Clear();

        for (int i = 0; i < costuraController.objFisicos.Count; i++)
        {
            costuraController.OBJFora.Add(costuraController.objFisicos[i]);
        }
        foreach (DragDrop item in allItems)
        {
            item.rectTransform.anchoredPosition =
                item.startPosition;
            //item.costuraController.OBJFora.Add(this);
        }
    }
}