using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] potentialDrops;
    public int[] dropWeight;
    public GameObject explosionPrefab;

    public int killValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Explode()
    {
        for(int i = 0; i < potentialDrops.Length; i++)
        {
            if(Random.Range(0,dropWeight[i]) == 0)
            {
                Instantiate(potentialDrops[i], transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
