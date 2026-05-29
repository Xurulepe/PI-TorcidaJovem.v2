using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MiniGame.TecInformatica
{
    public class InfoUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI infoText;

        [Header("Animation Settings")]
        [SerializeField] private float targetScale;
        [SerializeField] private float changeScaleDuration;
        [SerializeField] private float fadeToTransparentDuration;


        private void Start()
        {
            GameManager.Instance.OnPCChecked += UpdateText;

            infoText.color = Color.red;
            infoText.SetText("Coloque todos os componentes corretamente.");
            ResetText();
        }

        private void UpdateText()
        {
            if (!GameManager.Instance.HasGameFinished)
            {
                infoText.DOKill();

                infoText.SetText(GameManager.Instance.ComputerStatus.RequiredComponentText);
                infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 1f);
                infoText.transform.localScale = Vector3.one;

                infoText.transform.DOScale(targetScale, changeScaleDuration).SetLoops(2, LoopType.Yoyo).OnComplete(ResetText);
            }
        }

        private void ResetText()
        {
            float alphaValue = 0f;
            infoText.DOFade(alphaValue, fadeToTransparentDuration);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnPCChecked -= UpdateText;            
        }
    }
}
