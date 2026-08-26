using UnityEngine;

public class MusicalNote : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private bool wasHit = false;

    public bool WasHit => wasHit;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f * moveSpeed, transform.position.z);
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

    private void OnDisable()
    {
        wasHit = false;
    }
}
