using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : Healthbar
{
    /* Finds the child with the specified name of the object this script is attached to. */
    private void Awake()
    {
        fill = transform.Find("Fill").GetComponent<Image>();
        damageFill = transform.Find("FillDamage").GetComponent<Image>();
    }
}
