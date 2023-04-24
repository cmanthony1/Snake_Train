using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Input Referenece Data")]
    [SerializeField] private InputActionReference attackInput;

    [Header("Weapon Data")]
    [SerializeField] private WeaponData revolverData;
    [SerializeField] private Transform leftFirePoint;
    [SerializeField] private Transform rightFirePoint;

    private (Transform, Transform) firePoint;
    private bool canFire = true;
    private bool isTriggerDown = false;
    private bool fireFromLeft = true;

    /*
     * Subscription to Input Event. 
     * When the "Attack" input is performed (GetkeyDown), invoke Fire().
     */
    private void OnEnable()
    {
        attackInput.action.performed += TriggerDown;
        attackInput.action.canceled += TriggerUp;
    }

    private void Start()
    {
        firePoint = (leftFirePoint, rightFirePoint);
    }

    /* Unsubscribes from Input Event (if object is destroyed). */
    private void OnDisable()
    {
        attackInput.action.performed -= TriggerDown;
        attackInput.action.canceled -= TriggerUp;
    }

    private void Update()
    {
        if (isTriggerDown)
        {
            Fire();
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
                    projectile.transform.position = firePoint.Item1.transform.position;
                    projectile.transform.rotation = firePoint.Item1.transform.rotation;
                }
                else
                {
                    projectile.transform.position = firePoint.Item2.transform.position;
                    projectile.transform.rotation = firePoint.Item2.transform.rotation;
                }

                projectile.GetComponent<BulletController>().FireForce = revolverData.fireForce;
                int randomDamage = Mathf.FloorToInt(Random.Range(revolverData.minDamage, revolverData.maxDamage));
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

    private void TriggerDown(InputAction.CallbackContext context)
    {
        isTriggerDown = true;
    }

    private void TriggerUp(InputAction.CallbackContext context)
    {
        isTriggerDown = false;
    }

    private IEnumerator FireTimer()
    {
        canFire = false;
        yield return new WaitForSeconds(revolverData.fireRate);
        fireFromLeft = !fireFromLeft;
        canFire = true;
    }
}
