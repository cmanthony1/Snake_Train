using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Parent Class
public class Healthbar : MonoBehaviour
{
    protected Image fill;
    protected Image damageFill;
    protected const float FILL_TIMER_MAX = 1f;
    protected float fillTimer;
    protected float shrinkSpeed = 1f;

    private int effectState = 0;
    
    private void Update()
    {
        switch (effectState)
        {
            case 1:
                DamageEffect();
                break;
            case 2:
                HealEffect();
                break;
        }
    }

    /* Resets the timer and applies the adjusted amount to the UI element. */
    public void ShowDamage(float amount)
    {
        effectState = 1;
        fillTimer = FILL_TIMER_MAX;
        fill.fillAmount = amount;
    }

    public void ShowHeal(float amount)
    {
        effectState = 2;
        fillTimer = FILL_TIMER_MAX;
        damageFill.fillAmount = amount;
    }

    /*
     * Resets the timer, and applies the adjusted amount to the UI element.
     * Invoked by child classes.
     */
    public void DamageEffect()
    {
        fillTimer -= Time.deltaTime;

        if (fillTimer < 0)
        {
            if (fill.fillAmount < damageFill.fillAmount)
            {
                damageFill.fillAmount -= shrinkSpeed * Time.deltaTime;
            }

            fillTimer = 0;
        }
    }

    public void HealEffect()
    {
        fillTimer -= Time.deltaTime;

        if (fillTimer < 0)
        {
            if (damageFill.fillAmount > fill.fillAmount)
            {
                fill.fillAmount += shrinkSpeed * Time.deltaTime;
            }

            fillTimer = 0;
        }
    }
}
