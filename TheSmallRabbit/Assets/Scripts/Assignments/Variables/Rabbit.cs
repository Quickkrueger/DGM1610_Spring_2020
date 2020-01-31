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
    public float moveSpeed;
    public float rotateSpeed = 2;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
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

        if (!burrowed)
        {
            Move();
        }

        Rotate();

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
        gameObject.GetComponent<Collider>().enabled = true;
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
            gameObject.GetComponent<Collider>().enabled = false;
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

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().velocity = transform.forward * verticalMove * moveSpeed * Time.deltaTime + new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f) + transform.right * horizontalMove * moveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        
        yaw += rotateSpeed * Input.GetAxis("Mouse X");
        //pitch -= rotateSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
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
