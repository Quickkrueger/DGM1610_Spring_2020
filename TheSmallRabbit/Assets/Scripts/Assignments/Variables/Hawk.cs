using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : Predator
{
    public Transform flightPathCenter;
    private float flightAngle = 0;
    private float baseAngleIncrement;
    private float angleIncrement;
    private float baseFlightRadius = 100f;
    public float flightRadius = 0.0f;
    private float flightPathY;
    private bool exitingDive = false;
    int currentFlightPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (flightPathCenter == null)
        {
            try
            {
                flightPathCenter = FindObjectOfType<Burrow>().transform;
            }
            catch
            {
                flightPathCenter = FindObjectOfType<Pickup>().transform;
            }
        }
        attackDamage = 5;
        flightPathY = transform.position.y;
        GetComponent<Renderer>().material.color = coloration;
        baseAngleIncrement = (Mathf.PI / (flightRadius + baseFlightRadius));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!stunned && !caughtPrey && collision.gameObject.tag == "Rabbit")
        {
            GrabPrey();
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        Move();

        if (!inPursuit || caughtPrey)
        {
            MaintainFlightPath();
        }
        else
        {
            MaintainPursuit();
        }

        if(caughtPrey && canDamage)
        {
            GameManager.instance.HarmRabbit(5);
            canDamage = false;
            StartCoroutine(AttackCooldown());
        }

        if(caughtPrey && prey.GetComponent<Rabbit>().Escaped())
        {
            caughtPrey = false;
            canDamage = false;
            stunned = true;
            StartCoroutine(EscapeStun());
        }
    }

    private void MaintainFlightPath()
    {

        baseAngleIncrement = (Mathf.PI / (flightRadius + baseFlightRadius));

        angleIncrement = ((speed / baseSpeed) * baseAngleIncrement);
        flightAngle += angleIncrement;

        destination = new Vector3(flightPathCenter.position.x + Mathf.Cos(flightAngle) * flightRadius, flightPathY, flightPathCenter.position.z + Mathf.Sin(flightAngle) * flightRadius);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), 0.1f);
    }

    private void MaintainPursuit()
    {
        //transform.position = new Vector3(transform.position.x + (distanceX / distance) * Time.deltaTime * 5, transform.position.y + (distanceY / distance) * Time.deltaTime * 5, transform.position.z + (distanceZ / distance) * Time.deltaTime * 5);
        //transform.LookAt(prey.transform);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(prey.transform.position - transform.position + Vector3.up * 0.25f), (angleIncrement * speed)/baseAngleIncrement);
        if (!prey.GetComponent<Collider>().enabled && !exitingDive)
        {
            exitingDive = true;
            StartCoroutine(ExitDive());
        }
    }

    IEnumerator ExitDive()
    {
        yield return new WaitForSeconds(0.5f);
        inPursuit = false;
        exitingDive = false;
        prey = null;
        speed = 10;
    }


}