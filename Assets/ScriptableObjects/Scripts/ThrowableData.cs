using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Throwable")]
public class ThrowableData : ScriptableObject
{
    public float MaxDamage;
    public float MinDamage;
    public float TravelSpeed;
    public float BlastRadius;
}
