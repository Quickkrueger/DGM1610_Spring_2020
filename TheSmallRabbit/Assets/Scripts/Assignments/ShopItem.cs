using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : Item
{
    // Start is called before the first frame update
    public GameObject ItemUI;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && InventoryManager.instance.CheckGold() >= itemData.shopCost)
        {
            InventoryManager.instance.RemoveGold(itemData.shopCost);
            base.OnTriggerEnter(other);
        }
    }

    protected override void PickupEffect()
    {
        base.PickupEffect();
    }

    public override void InitializeItem(ItemScriptableObject itemDat)
    {
        base.InitializeItem(itemDat);
        ItemUI.transform.GetChild(0).GetComponent<Text>().text = itemData.id;
        ItemUI.transform.GetChild(1).GetComponent<Text>().text = itemData.shopCost.ToString();
    }
}
