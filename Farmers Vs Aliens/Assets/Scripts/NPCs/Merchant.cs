using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{

    public GameObject[] shopItems;
    public GameObject shopSlots;
    private int currentItemIndex = 0;

    private void Start()
    {
        SetupShop();
    }

    public void SetupShop()
    {
        if(shopSlots.transform.childCount >= shopItems.Length)
        {
            ConstantStock();
        }
        else
        {
            CyclingStock();
        }
    }

    private void ConstantStock()
    {
        for(int i = 0; i < shopItems.Length; i++)
        {
            GameObject newItem = Instantiate(shopItems[i]);
            Transform slotTransform = shopSlots.transform.GetChild(i);

            if (slotTransform.childCount == 0)
            {
                newItem.GetComponent<PickUp>().isShopItem = true;
                newItem.transform.parent = slotTransform;
                newItem.transform.position = slotTransform.position;
                newItem.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void CyclingStock()
    {
        int numSlots = shopSlots.transform.childCount;
        for(int i = 0; i < numSlots; i++)
        {
            GameObject newItem = Instantiate(shopItems[currentItemIndex]);
            Transform slotTransform = shopSlots.transform.GetChild(i);

            if (slotTransform.childCount == 0)
            {
                newItem.GetComponent<PickUp>().isShopItem = true;
                newItem.transform.parent = slotTransform;
                newItem.transform.position = slotTransform.position;
                newItem.GetComponent<Rigidbody>().isKinematic = true;

                if (currentItemIndex < shopItems.Length)
                {
                    currentItemIndex++;
                }
                else
                {
                    currentItemIndex = 0;
                }
            }
        }
    }
    
}
