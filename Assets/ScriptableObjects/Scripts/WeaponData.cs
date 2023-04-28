using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public GameObject Prefab;
    public float MaxDamage;
    public float MinDamage;
    public float FireForce;
    public float FireRate;
    public float Capacity;
}
