using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Bullet Pooler class is a Object Pooler, a design pattern that pre-instantiates 
 * all objects one needs at any specific momenet before gameplay. This design pattern is
 * is less intensive on the user's system as instantiating and destroying objects uses 
 * a lot more computing power. This design pattern stores the desired objects in a list
 * and handles their active state in scene. Instead of instantiating/destroying, other
 * classes retrieve the objects from the list, applies any methods or values to the
 * object, and enables them. See "BulletController" or "TextController."
 */
public class BulletPooler : MonoBehaviour
{
    public static BulletPooler current;

    [Header("Weapon Data")]
    [SerializeField] private WeaponData weaponData;
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
            GameObject obj = Instantiate(weaponData.Prefab);
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
            GameObject obj = Instantiate(weaponData.Prefab);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}
