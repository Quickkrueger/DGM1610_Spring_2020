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
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        GetComponent<Renderer>().material.color = coloration;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rabbit")
        {
            inPursuit = true;
            prey = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rabbit")
        {
            inPursuit = false;
            prey = null;
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
        float distance = Vector3.Distance(flightPathCenter.transform.position, new Vector3(transform.position.x, flightPathCenter.transform.position.y, transform.position.z));
        float distanceX = flightPathCenter.transform.position.x - transform.position.x;
        float distanceZ = flightPathCenter.transform.position.z - transform.position.z;

        destination = new Vector3(flightPathCenter.transform.position.x + distance * Mathf.Cos(Mathf.Acos(distanceX/ distance) + 0.0314f * Time.deltaTime) ,transform.position.y, flightPathCenter.transform.position.z + distance * Mathf.Sin(Mathf.Asin(distanceX / distance) + 00.314f * Time.deltaTime));

        transform.position = destination;
    }
}
