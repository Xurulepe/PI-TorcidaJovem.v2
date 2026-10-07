using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField] Color corRecorte;
    Image _image;
    [SerializeField] Sprite _sprite;
    public bool bloqueado = false;



    private void Awake()
    {
        _image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        costuraController = Camera.main.GetComponent<CosturaController>();

        startPosition = rectTransform.anchoredPosition;

        if (!costuraController.OBJFora.Contains(this))
            costuraController.OBJFora.Add(this);

        if (!costuraController.objFisicos.Contains(this))
            costuraController.objFisicos.Add(this);

        if (!allItems.Contains(this))
            allItems.Add(this);
        /*_image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        costuraController = Camera.main.GetComponent<CosturaController>();
        costuraController.OBJFora.Add(this);
        costuraController.objFisicos.Add(this);
        startPosition = rectTransform.anchoredPosition;
        allItems.Add(this);*/
        // MudarImgff();

    }
    private void OnDestroy()
    {
        allItems.Remove(this);

        if (costuraController != null)
        {
            costuraController.OBJFora.Remove(this);
            costuraController.OBJDentro.Remove(this);
            costuraController.objFisicos.Remove(this);
            costuraController.ObjInventario.Remove(this);
        }
    }

    public void MudarImgff()
    {
        _image.color = corRecorte;
        _image.sprite = _sprite;

        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (bloqueado)
            return;

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (bloqueado)
            return;

        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Está fora da DropArea
        if (!IsInsideArea())
        {
            Debug.Log("Fora da área!");
            ResetItem();
            return;
        }

        // Está dentro, mas colidiu com outro Image
        if (HasCollision())
        {
            Debug.Log("Colidiu com outro Image! Voltando.");
            ResetItem();
            return;
        }

        // Está dentro e não colidiu
        Debug.Log("Objeto colocado corretamente.");

        if (!costuraController.OBJDentro.Contains(this))
            costuraController.OBJDentro.Add(this);

        costuraController.OBJFora.Remove(this);

        if (costuraController.OBJFora.Count == 0)
        {
            costuraController.botaoFinal.SetActive(true);
        }

        bloqueado = true;
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
            // Não verifica contra ele mesmo
            if (item == this)
                continue;

            Rect otherRect = GetWorldRect(item.rectTransform);

            // Verifica se os dois Images estão se sobrepondo
            if (myRect.Overlaps(otherRect, true))
            {
                Debug.Log("COLISÃO COM: " + item.gameObject.name);
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
            item.bloqueado = false;
        }
    }
    public void ResetItem()
    {
        rectTransform.anchoredPosition = startPosition;
        bloqueado = false;

        if (!costuraController.OBJFora.Contains(this))
            costuraController.OBJFora.Add(this);

        costuraController.OBJDentro.Remove(this);
    }
}