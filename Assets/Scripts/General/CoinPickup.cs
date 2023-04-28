using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    /* 
     * This script attatches to the gold pickup prefabs. On contact with player, add gold 
     * to inventory and either set active or destroy self
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerInventory>().AddCoins++;
            Destroy(gameObject);
        }
    }
}
