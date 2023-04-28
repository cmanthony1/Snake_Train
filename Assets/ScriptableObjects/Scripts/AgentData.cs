using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Agent")]
public class AgentData : ScriptableObject
{
    public float MaxHealth;
    public float Attack;
    [Header("Mutiplied by 100")]
    public float MoveSpeed;
}
