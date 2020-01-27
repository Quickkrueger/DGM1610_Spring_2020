﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : MonoBehaviour
{
    private int attackDamage;
    public int speed;
    public Color coloration;
    public Transform flightPathCenter;
    private bool inPursuit = false;
    private bool hasDestination;
    private GameObject prey;
    private Vector3[] destination;
    public GameObject flightPath;
    private float flightError = 0.05f;
    int currentFlightPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        GetComponent<Renderer>().material.color = coloration;
        destination = new Vector3[8];
        destination[0] = new Vector3(flightPathCenter.position.x + 9, transform.position.y, flightPathCenter.position.z);
        destination[1] = new Vector3(flightPathCenter.position.x + (9 * Mathf.Sqrt(2))/2, transform.position.y, flightPathCenter.position.z + (9 * Mathf.Sqrt(2)) / 2);
        destination[2] = new Vector3(flightPathCenter.position.x, transform.position.y, flightPathCenter.position.z + 9);
        destination[3] = new Vector3(flightPathCenter.position.x - (9 * Mathf.Sqrt(2)) / 2, transform.position.y, flightPathCenter.position.z + (9 * Mathf.Sqrt(2)) / 2);
        destination[4] = new Vector3(flightPathCenter.position.x - 9, transform.position.y, flightPathCenter.position.z);
        destination[5] = new Vector3(flightPathCenter.position.x - (9 * Mathf.Sqrt(2)) / 2, transform.position.y, flightPathCenter.position.z - (9 * Mathf.Sqrt(2)) / 2);
        destination[6] = new Vector3(flightPathCenter.position.x, transform.position.y, flightPathCenter.position.z - 9);
        destination[7] = new Vector3(flightPathCenter.position.x + (9 * Mathf.Sqrt(2)) / 2, transform.position.y, flightPathCenter.position.z - (9 * Mathf.Sqrt(2)) / 2);
        hasDestination = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rabbit")
        {
            prey = other.gameObject;
            inPursuit = true;
        }
        //if(other.tag == "FlightPath")
        //{
        //    if(currentFlightPoint + 1 < flightPath.transform.childCount)
        //    {
        //        currentFlightPoint++;
        //        destination = flightPath.transform.GetChild(currentFlightPoint).position;
        //    }
        //    else
        //    {
        //        currentFlightPoint = 0;
        //        destination = flightPath.transform.GetChild(currentFlightPoint).position;
        //    }
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inPursuit)
        {
            MaintainFlightPath();
        }
        else
        {
            MaintainPursuit();
        }

    }

    private void MaintainFlightPath()
    {
        if (!hasDestination)
        {
            currentFlightPoint++;
            if(currentFlightPoint >= destination.Length)
            {
                currentFlightPoint = 0;
            }
            hasDestination = true;
        }
        float distanceX = destination[currentFlightPoint].x - transform.position.x;
        float distanceY = destination[currentFlightPoint].y - transform.position.y;
        float distanceZ = destination[currentFlightPoint].z - transform.position.z;
        float distance = Vector3.Distance(destination[currentFlightPoint], transform.position);

        transform.position = new Vector3(transform.position.x + (distanceX / distance) * Time.deltaTime * 5, transform.position.y + (distanceY / distance) * Time.deltaTime * 5, transform.position.z + (distanceZ / distance) * Time.deltaTime * 5);
        if(transform.position.x >= destination[currentFlightPoint].x - flightError && transform.position.x <= destination[currentFlightPoint].x + flightError && transform.position.z >= destination[currentFlightPoint].z - flightError && transform.position.z <= destination[currentFlightPoint].z + flightError)
        {
            hasDestination = false;
        }
    }

    private void MaintainPursuit()
    {
        float distanceX = prey.transform.position.x - transform.position.x;
        float distanceY = prey.transform.position.y + 0.5f - transform.position.y;
        float distanceZ = prey.transform.position.z - transform.position.z;
        float distance = Vector3.Distance(prey.transform.position, transform.position);
        transform.position = new Vector3(transform.position.x + (distanceX / distance) * Time.deltaTime * 5, transform.position.y + (distanceY / distance) * Time.deltaTime * 5, transform.position.z + (distanceZ / distance) * Time.deltaTime * 5);

        if (!prey.GetComponent<Collider>().enabled)
        {
            inPursuit = false;
            prey = null;
        }
    }
}
