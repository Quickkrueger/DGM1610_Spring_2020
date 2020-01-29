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
    private float timeInterval = 5;
    private bool jumping = false;
    // Start is called before the first frame update
    void Start()
    {
        InitializeRabbit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !burrowed && !jumping)
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
        timeInterval = timeInterval - Time.deltaTime;

        if(timeInterval <= 0)
        {
            currentHunger--;
            timeInterval = 5;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
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
        gameObject.transform.position = currentBurrow.transform.position + Vector3.up * 0.5f;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            currentHunger += 5;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Ground" && jumping)
        {
            jumping = false;
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
        jumping = true;
        GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 200, gameObject.transform.position);
    }

    private void MoveForward()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0) + transform.forward * 5;
    }

    private void TurnLeft()
    {
        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 2f, transform.rotation.eulerAngles.z);
    }

    private void TurnRight()
    {
        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 2f, transform.rotation.eulerAngles.z);
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
