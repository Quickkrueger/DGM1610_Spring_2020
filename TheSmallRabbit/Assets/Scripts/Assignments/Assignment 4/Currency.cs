using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Pickup
{
    public int currencyValue;
    // Start is called before the first frame update
    protected override void PickupEffect()
    {
        InventoryManager.instance.AddGold(currencyValue);
    }
}
