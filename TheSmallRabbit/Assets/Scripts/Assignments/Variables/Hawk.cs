using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : MonoBehaviour
{
    private int attackDamage;
    public int speed;
    public Color coloration;
    public GameObject flightPathCenter;
    private bool inPursuit = false;
    private GameObject prey;
    private Vector3 destination;
    public GameObject flightPath;
    int currentFlightPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        GetComponent<Renderer>().material.color = coloration;
        destination = flightPath.transform.GetChild(currentFlightPoint).position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FlightPath")
        {
            if(currentFlightPoint + 1 < flightPath.transform.childCount)
            {
                currentFlightPoint++;
                destination = flightPath.transform.GetChild(currentFlightPoint).position;
            }
            else
            {
                currentFlightPoint = 0;
                destination = flightPath.transform.GetChild(currentFlightPoint).position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPursuit)
        {
            MaintainFlightPath();
        }
        else
        {
            //Pursue prey
        }

    }

    private void MaintainFlightPath()
    {
        float distanceX = destination.x - transform.position.x;
        float distanceZ = destination.z - transform.position.z;
        float distance = Vector3.Distance(destination, transform.position);

        transform.position = new Vector3(transform.position.x + (distanceX / distance) * Time.deltaTime * 5, transform.position.y, transform.position.z + (distanceZ / distance) * Time.deltaTime * 5);

    }
}
