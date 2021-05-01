using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickUp
{
    protected override void PickUpEffect()
    {
        if (((WeaponScriptableObject)itemData).projectileSpeed != 0)
        {
            GameManager._instance.GetPlayer().GetComponent<PlayerController>().SwapWeapon((WeaponScriptableObject)itemData);
            base.PickUpEffect();
        }
    }
}
