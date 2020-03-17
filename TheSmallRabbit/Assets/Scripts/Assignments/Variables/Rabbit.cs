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
    public float jumpPower = 10;
    public float moveSpeed;
    private float moveSpeedMultiplier = 1.0f;
    public float rotateSpeed = 2;
    private float yaw = 0.0f;
    private bool isCaught = false;
    private int escapePoints = 0;
    private int escapeThreshold = 5;
    private Rigidbody rb;
    private Renderer rRenderer;
    private Collider rCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rRenderer = GetComponent<Renderer>();
        rCollider = GetComponent<Collider>();

        InitializeRabbit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !burrowed && !jumping && !isCaught)
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
                    rb.isKinematic = false;
                    rCollider.enabled = true;
                    transform.parent = null;
                    escapePoints = 0;
                }
            }
        }

        Rotate();

        if (Input.GetButtonDown("Use"))
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

        moveSpeedMultiplier = GameManager.instance.GetHungerRatio();
    }

    void InitializeRabbit()
    {
        rRenderer.material.color = coloration;
    }

    public void LeaveBurrow()
    {
        burrowed = false;
        currentBurrow.GetComponent<Burrow>().EjectOccupant();
        gameObject.transform.position = currentBurrow.transform.position + Vector3.up * 0.3f;
        rCollider.enabled = true;
        rb.useGravity = true;
        Jump();
    }

    private void OnTriggerStay(Collider collision)
    {
            if (collision.gameObject.tag == "Burrow" && !isCaught)
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


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && !jumping)
        {
            jumping = true;
        }
    }

    private void Burrow()
    {
        if (currentBurrow.GetComponent<Burrow>().AllowOccupant(gameObject.GetComponent<Rabbit>()))
        {
            rCollider.enabled = false;
            rb.useGravity = false;
            gameObject.transform.position = currentBurrow.transform.position;
            rb.velocity = Vector3.zero;
            burrowed = true;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower * 1000 * Time.deltaTime);
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        rb.velocity = transform.forward * verticalMove * moveSpeed * moveSpeedMultiplier * Time.deltaTime + new Vector3(0f, rb.velocity.y, 0f) + transform.right * horizontalMove * moveSpeed * moveSpeedMultiplier * Time.deltaTime;
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
        rb.isKinematic = true;
        transform.position = transform.parent.position;
        rCollider.enabled = false;
        currentBurrow = null;
    }

    public bool Escaped()
    {
        return !isCaught;
    }
}
