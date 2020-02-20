using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : MonoBehaviour
{
    private int attackDamage;
    public Color coloration;
    public float speed = 20;
    private float baseSpeed = 20;
    public Transform flightPathCenter;
    private bool inPursuit = false;
    private bool hasDestination;
    private GameObject prey;
    private Vector3 destination;
    private float flightAngle = 0;
    private float baseAngleIncrement;
    private float angleIncrement;
    private float baseFlightRadius = 100f;
    public float flightRadius = 0.0f;
    private float flightPathY;
    private bool caughtPrey = false;
    private bool exitingDive = false;
    private bool canDamage = true;
    private bool stunned = false;
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
    void FixedUpdate()
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

    private void GrabPrey()
    {
        caughtPrey = true;
        inPursuit = false;
        prey.transform.parent = transform.GetChild(0);
        prey.GetComponent<Rabbit>().Caught();
        speed = 10;
    }

    private void Move()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    IEnumerator ExitDive()
    {
        yield return new WaitForSeconds(0.5f);
        inPursuit = false;
        exitingDive = false;
        prey = null;
        speed = 10;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(3f);
        canDamage = true;
    }

    IEnumerator EscapeStun()
    {
        yield return new WaitForSeconds(5f);
        stunned = false;
        canDamage = true;
    }
}