using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public int idNum;
    public string id;
    public float cooldownTime;
    public Mesh itemMesh;
    public Sprite icon;
    public GameObject projectilePrefab;
    public bool toggles;
    public bool isFireArm;
}
