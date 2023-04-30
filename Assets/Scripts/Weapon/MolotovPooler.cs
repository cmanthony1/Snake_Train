using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovPooler : MonoBehaviour
{
    public static MolotovPooler current;

    [Header("Weapon Data")]
    [SerializeField] private GameObject pooledObject;
    [SerializeField] private float pooledSize;
    [SerializeField] private bool willGrow;

    private List<GameObject> pooledObjects;

    void Start()
    {
        current = this;
        pooledObjects = new List<GameObject>();

        /* Creates objects and adds to pooledObjects list. */
        for (int i = 0; i < pooledSize; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.hideFlags = HideFlags.HideInHierarchy;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    /* Returns object from pooledObjects list if it is not enabled. */
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        /* Option to have unlimited amount of pooled objects. */
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}
