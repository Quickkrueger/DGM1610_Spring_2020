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
        if (Input.GetKeyDown(KeyCode.Space) && !burrowed)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (burrowed)
            {
                LeaveBurrow();
            }
            else if(currentBurrow != null)
            {
                Burrow();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && !burrowed)
        {
            MoveForward();
        }

        if ((Input.GetKey(KeyCode.A)))
        {
            TurnLeft();
        }

        if ((Input.GetKey(KeyCode.D)))
        {
            TurnRight();
        }
    }

    void InitializeRabbit()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        GetComponent<Renderer>().material.color = coloration;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Burrow")
        {
            currentBurrow = null;
         }
    }

    private void Burrow()
    {
        if (currentBurrow.GetComponent<Burrow>().AllowOccupant(gameObject.GetComponent<Rabbit>()))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.transform.position = currentBurrow.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            burrowed = true;
        }
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 200, gameObject.transform.position);
    }

    private void MoveForward()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0) + transform.forward * 5;
    }

    private void TurnLeft()
    {
        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 1f, transform.rotation.eulerAngles.z);
    }

    private void TurnRight()
    {
        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 1f, transform.rotation.eulerAngles.z);
    }

    public bool IsBurrowed()
    {
        return burrowed;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetHunger()
    {
        return currentHunger;
    }
}
