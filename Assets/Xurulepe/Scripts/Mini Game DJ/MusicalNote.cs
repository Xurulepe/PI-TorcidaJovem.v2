using UnityEngine;

public class MusicalNote : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f * moveSpeed, transform.position.z);
    }
}
