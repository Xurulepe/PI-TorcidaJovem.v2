using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class AccuracyInfo : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private RectTransform noteContainer;
    [SerializeField] private TextMeshProUGUI accuracyText;

    private float notePosX;
    [SerializeField] private float finalPosY = 150f;

    [SerializeField] private float fadeOutDuration = 1.0f;


    //private void OnEnable()
    //{
    //    StartCoroutine(FadeOut());
    //}

    public void Play()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        accuracyText.DOFade(0f, fadeOutDuration);
        rectTransform.DOAnchorPos(new Vector2(notePosX, noteContainer.anchoredPosition.y + finalPosY), fadeOutDuration);

        yield return new WaitForSeconds(fadeOutDuration);

        accuracyText.alpha = 1f;
        gameObject.SetActive(false);
    }   
    
    public void Setup(string accuracyInfoText, float xPos)
    {
        noteContainer = GameManager.Instance.NoteContainer;
        accuracyText.text = accuracyInfoText;
        rectTransform.anchoredPosition = new Vector2(xPos, noteContainer.anchoredPosition.y);
        notePosX = xPos;
    }
}
