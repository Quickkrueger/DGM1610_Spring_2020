using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private ItemScriptableObject[] items;
    int nextIndex = 0;

    void Start()
    {
        instance = GetComponent<InventoryManager>();
        items = new ItemScriptableObject[10];
    }

    public void AddItemToInventory(ItemScriptableObject itemData)
    {
        items[nextIndex] = itemData;
        nextIndex++;
    }

    public ItemScriptableObject ItemToEquip(int inventorySlot)
    {
        if(nextIndex >= inventorySlot)
        {
            return items[inventorySlot - 1];
        }

        return null;
    }

}
