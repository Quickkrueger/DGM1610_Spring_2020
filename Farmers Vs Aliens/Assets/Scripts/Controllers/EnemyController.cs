using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] potentialDrops;
    public GameObject explosionPrefab;
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
            if(Random.Range(0,2) == 1)
            {
                Instantiate(potentialDrops[i], transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
