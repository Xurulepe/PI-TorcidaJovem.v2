using UnityEngine;

public class ResetPoolCollider : MonoBehaviour
{
    [SerializeField] private int notesLayerMaskInt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == notesLayerMaskInt)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
