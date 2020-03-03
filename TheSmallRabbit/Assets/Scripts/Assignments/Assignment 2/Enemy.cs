using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int attackDamage;
    public Color coloration;
    public float speed = 3;
    private int health;
    protected float baseSpeed = 20;
    protected bool canDamage = true;
    protected int maxHealth = 5;
    public Transform spawnPoint;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
        attackDamage = 5;
        GetComponent<Renderer>().material.color = coloration;
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;

    }



    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        Move();
    }


    protected virtual void Move()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }


    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(3f);
        canDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
            Debug.Log("Owwie, oof, ouchie.");
        }
        else
        {
            Debug.Log("DED.");
            health = maxHealth;
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;

        }
    }


}
