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
    protected bool inPursuit = false;
    protected bool hasDestination;
    protected GameObject prey;
    protected Vector3 destination;
    protected bool caughtPrey = false;
    protected bool canDamage = true;
    protected bool stunned = false;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        GetComponent<Renderer>().material.color = coloration;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!stunned && !caughtPrey && other.tag == "Rabbit")
        {
            prey = other.gameObject;
            inPursuit = true;
            speed = 20;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!stunned && !caughtPrey && collision.gameObject.tag == "Rabbit")
        {
            GrabPrey();
        }
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void GrabPrey()
    {
        caughtPrey = true;
        inPursuit = false;
        prey.transform.parent = transform.GetChild(0);
        prey.GetComponent<Rabbit>().Caught();
        speed = 10;
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

    protected IEnumerator EscapeStun()
    {
        yield return new WaitForSeconds(5f);
        stunned = false;
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
