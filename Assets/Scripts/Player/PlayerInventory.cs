using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //this holds the player's inventory. currently all that we have in here is gold, but we may later add gun types and/or ammo

    public int gold;

    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
    }

}
