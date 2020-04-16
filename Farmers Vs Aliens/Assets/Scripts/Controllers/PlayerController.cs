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
    public GameObject item;
    public GameObject bulletPrefab;
    public ItemScriptableObject currentItem;
    public int maxHealth;
    private int currentHealth;

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

    private void Update()
    {
        if (Input.GetButtonDown("P" + thisPlayerNum + " Jump") && grounded)
        {
            Jump();
        }

        if (currentItem != null)
        {
            if (item.transform.childCount <= 0)
            {
                GameObject newItem = Instantiate(currentItem.model);
                newItem.transform.parent = item.transform;
                newItem.transform.position = item.transform.position;
            }
            LookForTarget();
        }

        if (Input.GetButtonDown("P" + thisPlayerNum + " Fire1") && currentItem != null)
        {
            Fire();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

    private void LookForTarget()
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Equals("Enemy"))
            {
                PointWeapon(hit.collider.gameObject.transform);
            }
            else
            {
                item.transform.rotation = gameObject.transform.rotation;
            }
        }

    }

    private void PointWeapon(Transform target)
    {
        item.transform.LookAt(target);
    }

    private void Fire()
    {
        for(int i = 0; i < currentItem.numProjectile; i++)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, item.transform.position + item.transform.forward, item.transform.rotation);
            currentBullet.GetComponent<BulletController>().InitializeBullet(currentItem.hasSpread, currentItem.spreadRange, currentItem.projectileSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" && !grounded)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && grounded)
        {
            grounded = false;
        }
    }
}
