using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickUp
{
    public ItemScriptableObject weaponData;

    protected override void PickUpEffect()
    {
        GameManager._instance.GetPlayer().GetComponent<PlayerController>().SwapWeapon(weaponData);
        base.PickUpEffect();
    }
}
