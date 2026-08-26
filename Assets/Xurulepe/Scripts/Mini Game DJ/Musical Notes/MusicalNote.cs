using UnityEngine;

public class MusicalNote : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private RectTransform rectTransform;

    private bool wasHit = false;

    public bool WasHit => wasHit;

    private void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y * moveSpeed * Time.deltaTime, transform.position.z);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y -1f * moveSpeed * Time.deltaTime);
    }

    public void SetHit()
    {
        wasHit = true;
    }

    public void DeactiveSelf()
    {
        GameManager.Instance.IncrementDeactivatedNotesCount();
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    public Vector2 GetPosition()
    {
        return rectTransform.anchoredPosition;
    }

    private void OnDisable()
    {
        wasHit = false;
    }
}
