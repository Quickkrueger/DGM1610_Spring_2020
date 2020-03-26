using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : Item
{
    // Start is called before the first frame update
    public int cost = 0;
    public GameObject ItemUI;

    void Start()
    {
        ItemUI.transform.GetChild(0).GetComponent<Text>().text = itemData.id;
        ItemUI.transform.GetChild(1).GetComponent<Text>().text = cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && InventoryManager.instance.CheckGold() >= cost)
        {
            InventoryManager.instance.RemoveGold(cost);
            base.OnTriggerEnter(other);
        }
    }

    protected override void PickupEffect()
    {
        base.PickupEffect();
    }
}
