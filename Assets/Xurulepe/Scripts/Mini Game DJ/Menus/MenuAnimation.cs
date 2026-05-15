using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> buttonList = new ();
    [SerializeField] private List<GameObject> textList = new ();
    [SerializeField] private GameObject menuObject;

    [Header("Animation duration")]
    [SerializeField] private float buttonAnimationDuration;
    [SerializeField] private float textAnimationDuration;
    [SerializeField] private float menuAnimationDuration;

    private void OnEnable()
    {
        StartCoroutine(AnimateMenu());
    }

    private IEnumerator AnimateMenu()
    {
        menuObject.transform.localScale = Vector3.zero;

        Vector3 buttonsHideScale = new Vector3(0f, 1f, 1f);
        HideElements(buttonList, buttonsHideScale);

        Vector3 textsHideScale = new Vector3(1f, 0f, 1f);
        HideElements(textList, textsHideScale);


        menuObject.transform.DOScale(Vector3.one, menuAnimationDuration).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(menuAnimationDuration);

        AnimateElements(buttonList, buttonAnimationDuration);

        AnimateElements(textList, textAnimationDuration);
    }

    private void HideElements(List<GameObject> list, Vector3 scale)
    {
        foreach (GameObject element in list)
        {
            element.transform.localScale = scale;
        }
    }

    private void AnimateElements(List<GameObject> list, float duration)
    {
        foreach (GameObject element in list)
        {
            element.transform.DOScale(Vector3.one, duration);
        }
    }
}
