using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    static int totalPlayerNum = 0;

    public float moveSpeed;
    public float jumpPower;
    public Color[] hatColors;

    private int thisPlayerNum;
    private bool grounded = true;
    private bool usingMouse;
    private Rigidbody rb;
    void Start()
    {
        totalPlayerNum++;
        thisPlayerNum = totalPlayerNum;

        if (thisPlayerNum == 1)
        {
            usingMouse = true;
        }
        else
        {
            usingMouse = false;
        }

        rb = GetComponent<Rigidbody>();
        transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", hatColors[thisPlayerNum - 1]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("P" + thisPlayerNum + " Jump") && grounded)
        {
            Jump();
        }

        Move();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower * 1000 * Time.deltaTime);
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("P" + thisPlayerNum + " Horizontal");
        float verticalMove = Input.GetAxis("P" + thisPlayerNum + " Vertical");

        rb.velocity = Vector3.forward * verticalMove * moveSpeed * Time.deltaTime + new Vector3(0f, rb.velocity.y, 0f) + Vector3.right * horizontalMove * moveSpeed * Time.deltaTime;

        if(thisPlayerNum == 1 && (Mathf.Abs(Input.GetAxis("P1 Mouse X")) >= 0.01f || Mathf.Abs(Input.GetAxis("P1 Mouse Y")) >= 0.01f))
        {
            usingMouse = true;
        }
        else if ((Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick X")) >= 0.05f || Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick Y")) >= 0.05f))
        {
            usingMouse = false;
        }

        if (usingMouse) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mousePos.y;
            mousePos.y = transform.position.y;
            //mousePos.y = 0;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            objectPos.z = objectPos.y;
            objectPos.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(mousePos - objectPos);
            //transform.LookAt(mousePos - objectPos);
        }
        else if(!usingMouse && (Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick X")) >= 0.05f || Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick Y")) >= 0.05f))
        {
            Vector3 joystickPos = new Vector3(Input.GetAxisRaw("P" + thisPlayerNum + " Joystick X"), 0f, Input.GetAxisRaw("P" + thisPlayerNum + " Joystick Y"));



            transform.LookAt(joystickPos + transform.position);
        }
        
    }
}
