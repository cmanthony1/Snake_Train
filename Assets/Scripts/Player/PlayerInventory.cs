using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /* 
     * This holds the player's inventory. Currently all that we have in here is gold, but we may later add gun types and/or ammo. 
     * It also holds the gold prefabs, for the moment.
     */
    [SerializeField] private ItemData coinData;
    
    void Start()
    {
        coinData.Quantity = 0;
    }

    /* Getter/Setter */
    public int AddCoins
    {
        get { return coinData.Quantity; }
        set { coinData.Quantity += value; }
    }
}
