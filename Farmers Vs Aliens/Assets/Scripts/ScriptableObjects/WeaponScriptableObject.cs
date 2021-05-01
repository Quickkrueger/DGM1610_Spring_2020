using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
public class WeaponScriptableObject : ItemScriptableObject
{
    public AudioClip gunSound;
    public float cooldownTime;
    public int numProjectile;
    public float projectileSpeed;
    public bool hasSpread;
    [Range(0f, 1f)]
    public float spreadRange;
}
