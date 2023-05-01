using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] private GunData revolverData;
    [SerializeField] private Transform leftFirePoint;
    [SerializeField] private Transform rightFirePoint;

    private int sceneState;
    private bool canFire = true;
    private bool fireFromLeft = true;

    /*
     * Subscribes to the SendSceneState event in the CombatStateManager script.
     * Invokes: SceneState().
     */
    private void OnEnable()
    {
        CombatStateManager.SendSceneState += SceneState;
    }

    /* Unsubscribes from the SendSceneState event in the CombatStateManager script (if destroyed). */
    private void OnDisable()
    {
        CombatStateManager.SendSceneState += SceneState;
    }

    private void Update()
    {
        switch (sceneState)
        {
            case 1:
            case 2:
                
                if (Input.GetMouseButton(0))
                {
                    Fire();
                }

                break;
        }
    }

    /* 
     * Uses Object Pooler Design Pattern to enable and disable bullet objects. 
     * Gets bullet object from pooler, sets position, rotation, and force to rigidbody.
     */
    private void Fire()
    {
        if (canFire)
        {
            GameObject projectile = BulletPooler.current.GetPooledObject();

            if (projectile != null)
            {
                if (fireFromLeft)
                {
                    projectile.transform.position = leftFirePoint.transform.position;
                    projectile.transform.rotation = leftFirePoint.transform.rotation;
                }
                else
                {
                    projectile.transform.position = rightFirePoint.transform.position;
                    projectile.transform.rotation = rightFirePoint.transform.rotation;
                }

                projectile.GetComponent<BulletController>().FireForce = revolverData.FireForce;
                int randomDamage = Mathf.FloorToInt(Random.Range(revolverData.MinDamage, revolverData.MaxDamage));
                projectile.GetComponent<BulletController>().BulletDamage = randomDamage;
                projectile.SetActive(true);
                StartCoroutine(FireTimer());
            }
            else
            {
                return;
            }
        }
    }

    /* Fire Rate Control. */
    private IEnumerator FireTimer()
    {
        canFire = false;
        yield return new WaitForSeconds(revolverData.FireRate);
        fireFromLeft = !fireFromLeft;
        canFire = true;
    }

    private void SceneState(int state) 
    { 
        sceneState = state; 
    }
}
