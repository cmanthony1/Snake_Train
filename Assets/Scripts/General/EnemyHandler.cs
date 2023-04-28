using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private int enemyCounter;

    public static Action OnEnemiesDefeated;

    /*
     * Subscribes to the OnCreate event in the EnemyStats script.
     * Subscribes to the OnDestroy event in the EnemyStats script.
     * Invokes: IncreaseEnemyCount() and DecreaseEnemyCount()
     */
    private void OnEnable()
    {
        EnemyStats.OnCreate += IncreaseEnemyCount;
        EnemyStats.OnDestroy += DecreaseEnemyCount;
    }

    /*
    * Unsubscribes from the OnCreate event in the EnemyStats script (if destroyed).
    * Unsubscribes from the OnDestroy event in the EnemyStats script (if destroyed).
    */
    private void OnDisable()
    {
        EnemyStats.OnCreate -= IncreaseEnemyCount;
        EnemyStats.OnDestroy -= DecreaseEnemyCount;
    }

    private void Update()
    {
        if (enemyCounter <= 0)
        {
            /*
             * Calls all functions subscribed to this event.
             * Subscription: PromptTransition, SceneLoader.
             */
            OnEnemiesDefeated?.Invoke();
        }
    }

    private void IncreaseEnemyCount() { enemyCounter++; }
    private void DecreaseEnemyCount() { enemyCounter--; }
}
