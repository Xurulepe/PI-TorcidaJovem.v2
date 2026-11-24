using UnityEngine;

public class AccuracyInfoPool : MonoBehaviour
{
    public static AccuracyInfoPool Instance { get; private set; }

    [SerializeField] private ObjectPool accuracyPool;


    private void Awake()
    {
        Instance = this;

        accuracyPool.SetupPool();
    }

    public GameObject GetPooledObject()
    {
        return accuracyPool.GetPooledObject();
    }
}
