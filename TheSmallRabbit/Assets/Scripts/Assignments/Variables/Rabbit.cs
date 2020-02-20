using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    // Rabbit data
    public string rabbitName;
    
    public Color coloration;
    private bool burrowed = false;
    private GameObject currentBurrow;
    private bool jumping = false;
    public float moveSpeed;
    public float rotateSpeed = 2;
    private float yaw = 0.0f;
    private bool isCaught = false;
    private int escapePoints = 0;
    private int escapeThreshold = 5;
    // Start is called before the first frame update
    void Start()
    {
        InitializeRabbit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !burrowed && !jumping && !isCaught)
        {
            Jump();
        }

        if (!burrowed && !isCaught)
        {
            Move();
        }

        if (isCaught)
        {
            if (Input.anyKeyDown)
            {
                escapePoints++;
                if(escapePoints >= escapeThreshold)
                {
                    isCaught = false;
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<Collider>().enabled = true;
                    transform.parent = null;
                    escapePoints = 0;
                }
            }
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

    void InitializeRabbit()
    {
        GetComponent<Renderer>().material.color = coloration;
    }

    public void LeaveBurrow()
    {
        burrowed = false;
        currentBurrow.GetComponent<Burrow>().EjectOccupant();
        gameObject.transform.position = currentBurrow.transform.position + Vector3.up * 0.3f;
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
        if(collision.gameObject.tag == "Ground" && jumping)
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

    public void Caught()
    {
        isCaught = true;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = transform.parent.position;
        GetComponent<Collider>().enabled = false;
    }

    public bool Escaped()
    {
        return !isCaught;
    }
}
