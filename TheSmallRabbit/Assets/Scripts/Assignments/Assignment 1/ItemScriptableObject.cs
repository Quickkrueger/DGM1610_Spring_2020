using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public string ID;
    public float cooldownTime;
    public Mesh itemMesh;
    public Sprite icon;

}
