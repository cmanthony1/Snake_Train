using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //this holds the player's inventory. currently all that we have in here is gold, but we may later add gun types and/or ammo
    //it also holds the gold prefabs, for the moment

    public int gold;
    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
    }

}
