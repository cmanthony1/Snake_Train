using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField] GameObject coinObject;
    [SerializeField] int coinsDropped;

    /* Drops coins when enemy/object is destroyed. */
    public void DropCoins()
    {
        for (int a = 0; a < coinsDropped; a++)
        {
            GameObject place = Instantiate(coinObject);
            Vector2 pos = Random.insideUnitCircle * 1;
            place.transform.position = new Vector3(gameObject.transform.position.x + pos.x, gameObject.transform.position.y + (pos.y/2), 1f);
        }
    }

    private void OnDestroy()
    {
        DropCoins();
    }
}
