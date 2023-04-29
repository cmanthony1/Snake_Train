using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] private WeaponData revolverData;
    [SerializeField] private Transform leftFirePoint;
    [SerializeField] private Transform rightFirePoint;

    private (Transform, Transform) firePoint;
    private bool canFire = true;
    private bool fireFromLeft = true;

    private void Start()
    {
        firePoint = (leftFirePoint, rightFirePoint);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
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
}
