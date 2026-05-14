using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class ObjectPool
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public Transform parent;

    public void SetupPool()
    {
        pooledObjects = new List<GameObject>();

        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            tmp = GameObject.Instantiate(objectToPool);
            tmp.transform.SetParent(parent);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
