using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    //drops coins when enemy/object is destroyed

    //how many coins are dropped?
    [SerializeField] int coinsDropped;

    public void DropCoins()
    {
        GameObject thePrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().coinPrefab;

        for(int a = 0; a < coinsDropped; a++)
        {
            GameObject place = Instantiate(thePrefab);
            Vector2 pos = Random.insideUnitCircle * 1;
            place.transform.position = new Vector3(gameObject.transform.position.x + pos.x,gameObject.transform.position.y + pos.y,1f);
        }
    }

    private void OnDestroy()
    {
        DropCoins();
    }
}
