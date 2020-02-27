using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int attackDamage;
    public Color coloration;
    public float speed = 20;
    public int health = 20;
    protected float baseSpeed = 20;
    protected bool canDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        GetComponent<Renderer>().material.color = coloration;

    }



    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        Move();
    }


    protected void Move()
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
        }
    }
}
