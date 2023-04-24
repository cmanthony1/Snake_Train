using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Agent")]
public class AgentData : ScriptableObject
{
    public float maxHealth;
    public float attack;
    [Header("Mutiplied by 100")]
    public float moveSpeed;
}
