using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.TecInformatica
{
    public class InventorySlotUI : MonoBehaviour
    {
        [Header("Inventory Slot UI Settings")]
        [SerializeField] private Image image;
        [SerializeField] private float flashDuration;

        private float originalAlphaValue;

        private InventorySlot inventorySlot;

        private void Start()
        {
            inventorySlot = GetComponent<InventorySlot>();

            originalAlphaValue = image.color.a;

            inventorySlot.OnWrongItemPlaced += BlinkImage;
        }

        private void BlinkImage()
        {
            StartCoroutine(FlashColor());
        }

        private IEnumerator FlashColor()
        {
            int flashQuantity = 3;

            for (int i = 0; i < flashQuantity; i++)
            {
                image.DOFade(1f, flashDuration);

                yield return new WaitForSeconds(flashDuration);

                image.DOFade(originalAlphaValue, flashDuration);

                yield return new WaitForSeconds(flashDuration);
            }
        }

        private void OnDisable()
        {
            inventorySlot.OnWrongItemPlaced -= BlinkImage;
        }
    }
}
