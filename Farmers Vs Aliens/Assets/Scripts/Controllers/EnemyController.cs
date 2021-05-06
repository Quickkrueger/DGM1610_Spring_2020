using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] potentialDrops;
    public int[] dropWeight;
    public GameObject explosionPrefab;

    public int killValue;


    public int maxHealth = 10;
    protected int health;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
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
        GameManager._instance.GetPlayer().GetComponent<PlayerController>().GainMoney(killValue);
        SpawnManager._instance.EnemyDestroyed();
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            Explode();
        }
    }
}
