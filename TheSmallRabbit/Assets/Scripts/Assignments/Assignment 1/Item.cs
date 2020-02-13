using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Pickup
{
    public ItemScriptableObject itemData;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = itemData.itemMesh;
    }

    protected override void PickupEffect()
    {
        InventoryManager.instance.AddItemToInventory(itemData);
        Destroy(gameObject);
    }
}
