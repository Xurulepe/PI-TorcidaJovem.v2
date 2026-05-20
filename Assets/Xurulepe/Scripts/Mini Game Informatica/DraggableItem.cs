using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniGame.TecInformatica
{
    public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [Header("Draggable Item Settings")]
        [SerializeField] private Image image;
        [SerializeField] private float onDragScaleMultiplier = 1.25f;
        [SerializeField] private float moveDuration = 0.05f;
        [SerializeField] private ItemType itemType;

        [SerializeField] private bool hasSlots = false;
        [SerializeField] private List<GameObject> slotList = new List<GameObject>();

        private Vector3 originalScale;
        private Transform parentAfterDrag;
        private Tween moveTween;
        private InventorySlot inventorySlot;

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
            inventorySlot = transform.parent.GetComponent<InventorySlot>();
        }

        private void Start()
        {
            GameManager.Instance.OnPCChecked += CheckForCorrectSlot;
            GameManager.Instance.IncreaseIncorrectItemCount();
        }

        private void CheckForCorrectSlot()
        {
            if (inventorySlot.RequiredItem != itemType)
            {
                GameManager.Instance.IncreaseIncorrectItemCount();
            }
        }

        private void KillMoveTween()
        {
            moveTween.Kill();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DeactiveSlots();

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

            if (hasSlots)
            {
                ManageSlots();
            }
        }

        private void ManageSlots()
        {
            InventorySlot inventorySlot = parentAfterDrag.GetComponent<InventorySlot>();
            bool itemTypeMatchesRequiredItem = inventorySlot.RequiredItem != ItemType.None && inventorySlot.RequiredItem == itemType;

            if (itemTypeMatchesRequiredItem || HasAnyItemInAnySlot())
            {
                UnlockNewSlots();
            }
            else
            {
                DeactiveSlots();
            }
        }

        private void UnlockNewSlots()
        {
            foreach (GameObject slot in slotList)
            {
                slot.SetActive(true);
            }
        }

        private void DeactiveSlots()
        {
            foreach (GameObject slot in slotList)
            {
                slot.SetActive(false);
            }
        }

        private bool HasAnyItemInAnySlot()
        {
            bool hasItemInAnySlot = false;

            foreach (GameObject slot in slotList)
            {
                if (slot.transform.childCount > 0)
                {
                    hasItemInAnySlot = true;
                }
            }

            return hasItemInAnySlot;
        }

        public void SetInventorySlot(InventorySlot inventorySlot)
        {
            this.inventorySlot = inventorySlot;
        }
    }
}
