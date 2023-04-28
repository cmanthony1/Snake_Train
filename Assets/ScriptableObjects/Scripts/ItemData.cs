using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData: ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public float Value;
    public int Quantity;
}
