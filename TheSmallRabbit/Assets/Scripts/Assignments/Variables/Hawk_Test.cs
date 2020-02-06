using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk_Test : MonoBehaviour
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
    private float flightError = 1f;
    private float flightAngle = 0;
    private float baseAngleIncrement = (Mathf.PI / 100);
    private float angleIncrement;
    public float flightRadius = 12.0f;
    private bool caughtPrey = false;
    int currentFlightPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        GetComponent<Renderer>().material.color = coloration;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rabbit")
        {
            prey = other.gameObject;
            inPursuit = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rabbit")
        {
            GrabPrey();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        //if (!inPursuit || caughtPrey)
        //{
            MaintainFlightPath();
        //}
        //else
        //{
        //    MaintainPursuit();
        //}
    }

    private void MaintainFlightPath()
    {
        angleIncrement = (speed / baseSpeed) * baseAngleIncrement;
        flightAngle += angleIncrement;
        destination = new Vector3(flightPathCenter.position.x + Mathf.Cos(flightAngle) * flightRadius, transform.position.y, flightPathCenter.position.z + Mathf.Sin(flightAngle) * flightRadius);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), 0.1f);
    }

    private void MaintainPursuit()
    {

    }

    private void GrabPrey()
    {
        caughtPrey = true;
        prey.transform.parent = transform.GetChild(0);
        prey.GetComponent<Rabbit>().Caught();
    }

    private void Move()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

}
