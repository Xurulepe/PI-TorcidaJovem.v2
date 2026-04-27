using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Draggable Item Settings")]
    [SerializeField] private Image image;
    [SerializeField] private float onDragScaleMultiplier = 1.25f;
    [SerializeField] private float moveDuration = 0.05f;
    [SerializeField] private ItemType itemType;

    private Vector3 originalScale;
    private Transform parentAfterDrag;
    private Tween moveTween;

    public ItemType ItemType => itemType;
    public Transform ParentAfterDrag
    {
        get { return parentAfterDrag; }
        set 
        { 
            if (value != null) 
                parentAfterDrag = value; 
        }
    }

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void KillMoveTween()
    {
        moveTween.Kill();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.localScale *= onDragScaleMultiplier;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveTween = transform.DOMove(eventData.position, moveDuration).SetEase(Ease.OutBounce);
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        moveTween = transform.DOMove(parentAfterDrag.position, moveDuration).OnComplete(KillMoveTween);

        transform.position = eventData.position;
        transform.SetParent(parentAfterDrag);
        transform.localScale = originalScale;

        image.raycastTarget = true;        
    }
}
