using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Enemy
{
    // Start is called before the first frame update


    protected bool inPursuit = false;
    protected bool hasDestination;
    protected GameObject prey;
    protected Vector3 destination;
    protected bool caughtPrey = false;
    protected bool stunned = false;
    public int pursuitSpeed;

    protected IEnumerator EscapeStun()
    {
        yield return new WaitForSeconds(5f);
        stunned = false;
        canDamage = true;
    }

    protected void GrabPrey()
    {
        caughtPrey = true;
        inPursuit = false;
        prey.transform.parent = transform.GetChild(0);
        prey.GetComponent<Rabbit>().Caught();
        speed = 10;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (!stunned && !caughtPrey && collision.gameObject.tag == "Rabbit")
        {
            prey = collision.gameObject;
            GrabPrey();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!stunned && !caughtPrey && other.tag == "Rabbit")
        {
            prey = other.gameObject;
            inPursuit = true;
            speed = pursuitSpeed;
        }
    }

    protected virtual void MaintainPursuit()
    {
        //transform.position = new Vector3(transform.position.x + (distanceX / distance) * Time.deltaTime * 5, transform.position.y + (distanceY / distance) * Time.deltaTime * 5, transform.position.z + (distanceZ / distance) * Time.deltaTime * 5);
        //transform.LookAt(prey.transform);
        transform.LookAt(prey.transform);        
    }
}
