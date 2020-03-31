using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Pickup
{
    protected ItemScriptableObject itemData;
    // Start is called before the first frame update
    void Start()
    {
        if (itemData != null)
        {
            InitializeItem(itemData);
        }
    }

    protected override void PickupEffect()
    {
        InventoryManager.instance.AddItemToInventory(itemData);
        Destroy(gameObject);
    }

    public virtual void InitializeItem(ItemScriptableObject itemDat)
    {
        itemData = itemDat;
        GetComponent<MeshFilter>().mesh = itemData.itemMesh;
    }

}
