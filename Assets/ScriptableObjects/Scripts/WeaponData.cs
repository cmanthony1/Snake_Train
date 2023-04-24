using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public GameObject projectile;
    public float maxDamage;
    public float minDamage;
    public float fireForce;
    public float fireRate;
    public float capacity;
}
