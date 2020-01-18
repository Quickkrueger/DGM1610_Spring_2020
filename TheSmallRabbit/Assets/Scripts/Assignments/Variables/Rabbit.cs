using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    // Rabbit data
    public string rabbitName;
    public int maxHealth;
    public int maxHunger;
    public Color coloration;
    private int currentHealth;
    private int currentHunger;
    private bool burrowed = false;
    private GameObject currentBurrow;
    // Start is called before the first frame update
    void Start()
    {
        InitializeRabbit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (burrowed)
            {
                LeaveBurrow();
            }
            else
            {
                Burrow();
            }
        }
    }

    void InitializeRabbit()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        GetComponent<Renderer>().material.color = coloration;
        Debug.Log("How much health we got? " + currentHealth);
    }

    public void LeaveBurrow()
    {
        burrowed = false;
        currentBurrow.GetComponent<Burrow>().EjectOccupant();
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        Jump();
    }

    private void OnTriggerStay(Collider collision)
    {
            if (collision.gameObject.tag == "Burrow")
            {
                    currentBurrow = collision.gameObject;
            }
    }
    private void Burrow()
    {
        if (currentBurrow.GetComponent<Burrow>().AllowOccupant(gameObject.GetComponent<Rabbit>()))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.transform.position = currentBurrow.transform.position;
            burrowed = true;
        }
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 200, gameObject.transform.position);
    }

    public bool IsBurrowed()
    {
        return burrowed;
    }
}
