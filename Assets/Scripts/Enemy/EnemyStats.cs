using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [Header("Agent Data")]
    [SerializeField] private AgentData enemyData;

    private GameObject enemyHealthBarObj;
    private EnemyHealthBar enemyHealthBar;
    private float health;
    private Transform textOriginTransform;

    /* Finds the child with the specified name of the object this script is attached to. */
    private void Awake()
    {
        health = enemyData.maxHealth;
        enemyHealthBarObj = transform.Find("Canvas/Enemy Health Bar").gameObject;
        enemyHealthBar = enemyHealthBarObj.GetComponent<EnemyHealthBar>();
        textOriginTransform = transform.Find("FloatingTextOrigin").GetComponent<Transform>();

        enemyHealthBarObj.SetActive(false);
    }

    //Subtracts the damage from health and calls a function that sends the remaining health to its own UI element.
    public void TakeDamage(float value)
    {
        health -= value;

        /* Checks if the health is over the max health of the agent. */
        if (health > enemyData.maxHealth)
        {
            health = enemyData.maxHealth;
        }

        /* Sets the enemy health bar to active once it recieves damage. */
        if (health != enemyData.maxHealth)
        {
            enemyHealthBarObj.SetActive(true);
        }

        ShowValue(value);

        float adjustedHealth = (health / enemyData.maxHealth);
        enemyHealthBar.ShowDamage(adjustedHealth);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    /* Retrieves text object from TextPooler and applies properties. */
    private void ShowValue(float value)
    {
        GameObject floatingTextObj = TextPooler.current.GetPooledObject();
        floatingTextObj.GetComponentInChildren<TMP_Text>().text = value.ToString();
        floatingTextObj.transform.position = textOriginTransform.position;
        floatingTextObj.transform.rotation = Quaternion.identity;
        floatingTextObj.SetActive(true);
    }
}
