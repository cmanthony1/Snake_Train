using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldPickup : MonoBehaviour
{
    //this script attatches to the gold pickup prefabs

    //on contact with player, add gold to inventory and either set active or destroy self
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("player hit?");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerInventory>().gold++;
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
