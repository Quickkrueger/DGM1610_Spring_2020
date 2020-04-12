using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : NPC
{
    public ItemScriptableObject[] wares;
    private GameObject[] wareInstances;
    public GameObject shopItemPrefab;
    public float waresYSpawnValue;
    private Vector3 currentItemPosition;
    public Transform shopEdge;
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
        currentItemPosition = transform.position + Vector3.right * 2;
        float itemDistance = (shopEdge.position.x - (transform.position.x + 2)) / (wares.Length - 1);
        for (int i = 0; i < wares.Length; i++)
        {
            wareInstances[i] = Instantiate(shopItemPrefab, currentItemPosition, shopItemPrefab.transform.rotation);
            wareInstances[i].GetComponent<ShopItem>().InitializeItem(wares[i]);
            MeshCollider newCollider = wareInstances[i].AddComponent<MeshCollider>();
            newCollider.convex = true;
            newCollider.isTrigger = true;
            currentItemPosition += Vector3.right * itemDistance;

    }
    }
}
