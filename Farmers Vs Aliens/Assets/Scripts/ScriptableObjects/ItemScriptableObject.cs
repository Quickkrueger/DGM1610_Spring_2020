using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public int idNum;
    public string name;
    public GameObject model;
    public float cooldownTime;
    public GameObject projectilePrefab;

}
