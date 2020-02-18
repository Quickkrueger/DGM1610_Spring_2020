using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private ItemScriptableObject[] items;
    int equippedSlot = 1;
    int nextIndex = 0;
    public Image[] icons;
    public GameObject hotbar;

    void Start()
    {
        instance = GetComponent<InventoryManager>();
        items = new ItemScriptableObject[10];
        ItemToEquip(equippedSlot);
    }

    public void AddItemToInventory(ItemScriptableObject itemData)
    {
        items[nextIndex] = itemData;
        icons[nextIndex].sprite = items[nextIndex].icon;
        nextIndex++;
    }

    public ItemScriptableObject ItemToEquip(int inventorySlot)
    {

        for (int i = 0; i < hotbar.transform.childCount; i++)
        {
            if (i != inventorySlot - 1)
            {
                hotbar.transform.GetChild(i).GetComponent<Outline>().effectColor = Color.black;
            }
            else
            {
                hotbar.transform.GetChild(inventorySlot - 1).GetComponent<Outline>().effectColor = Color.red;
            }
        }

        if (nextIndex >= inventorySlot)
        {
            return items[inventorySlot - 1];
        }
        return null;
    }

}
