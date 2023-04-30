using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : Healthbar
{
    /* Finds the child with the specified name of the object this script is attached to. */
    private void Awake()
    {
        fill = transform.Find("Fill").GetComponent<Image>();
        damageFill = transform.Find("FillDamage").GetComponent<Image>();
    }

    /*
     * Subscribes to the DamagePlayer event in the PlayerStats script.
     * Subscribes to the HealPlayer event in the PlayerStats script.
     * Invokes: Healthbar -> ShowDamage().
     * Invokes: Healthbar -> ShowHeal().
     */
    private void OnEnable()
    {
        PlayerStats.OnDamagePlayer += ShowDamage;
        PlayerStats.OnHealPlayer += ShowHeal;
    }

    /*
     * Unsubscribes from the DamagePlayer event in the PlayerStats script (if destroyed).
     * Unsubscribes from the HealPlayer event in the PlayerStats script (if destroyed).
     */
    private void OnDisable()
    {
        PlayerStats.OnDamagePlayer -= ShowDamage;
        PlayerStats.OnHealPlayer -= ShowHeal;
    }
}
