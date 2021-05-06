using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isShopItem;
    public ItemScriptableObject itemData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            GetComponent<Rigidbody>().useGravity = false;
            Collider[] colliders = GetComponentsInChildren<Collider>();
            for(int i = 0; i < colliders.Length; i++)
            {
                colliders[i].isTrigger = true;
            }
        }

        if(other.tag == "Player")
        {
            PickUpEffect(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpEffect(false);
        }
    }

    protected virtual void PickUpEffect(bool purchaseFulfilled)
    {
        if (isShopItem && (purchaseFulfilled || GameManager._instance.GetPlayer().GetComponent<PlayerController>().LoseMoney(itemData.price)))
        {
            Destroy(gameObject);
        }
        else if(!isShopItem)
        {
            Destroy(gameObject);
        }
    }
}
