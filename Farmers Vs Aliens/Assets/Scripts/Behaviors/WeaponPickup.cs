using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickUp
{
    protected override void PickUpEffect(bool purchaseFulfilled)
    {
        if (((WeaponScriptableObject)itemData).projectileSpeed != 0)
        {
            if (this.isShopItem && GameManager._instance.GetPlayer().GetComponent<PlayerController>().LoseMoney(itemData.price))
            {
                GameManager._instance.GetPlayer().GetComponent<PlayerController>().SwapWeapon((WeaponScriptableObject)itemData);
                base.PickUpEffect(true);
            }
            else if(!isShopItem)
            {
                GameManager._instance.GetPlayer().GetComponent<PlayerController>().SwapWeapon((WeaponScriptableObject)itemData);
                base.PickUpEffect(false);
            }
        }
    }
}
