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

    private void OnCollisionEnter(Collision collision)
    {
        if (!stunned && !caughtPrey && collision.gameObject.tag == "Rabbit")
        {
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
}
