using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : NPC
{
    public ItemScriptableObject[] wares;
    private GameObject[] wareInstances;
    public GameObject shopItemPrefab;
    public float waresYSpawnValue;
    public float waresSpacing;
    private Vector3 currentItemPosition;
    // Start is called before the first frame update
    void Start()
    {
        wareInstances = new GameObject[wares.Length];
        InitializeWares();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeWares()
    {
        currentItemPosition = transform.position;
        for (int i = 0; i < wares.Length; i++)
        {
            currentItemPosition += Vector3.right * waresSpacing;
            wareInstances[i] = Instantiate(shopItemPrefab, currentItemPosition, shopItemPrefab.transform.rotation);
            wareInstances[i].GetComponent<ShopItem>().InitializeItem(wares[i]);
            MeshCollider newCollider = wareInstances[i].AddComponent<MeshCollider>();
            newCollider.convex = true;
            newCollider.isTrigger = true;
            
        }
    }
}
