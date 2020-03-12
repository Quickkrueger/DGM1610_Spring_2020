using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jumping = false; // Like grounded but the opposite
    public float moveSpeed;
    public float rotateSpeed = 2;
    public float jumpPower = 10;
    public GameObject projectilePrefab;
    public GameObject bobberPrefab;
    public GameObject heldItem;
    private ItemScriptableObject equippedItem;
    private bool firearmEquipped = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            Jump();
        }

        Move();

        if (Input.GetButton("Fire1") && equippedItem != null)
        {
            UseItem();
        }

        if (Input.GetButtonDown("Use"))
        {
            //Interact();   
        }
        if (Input.anyKey)
        {
            EquipItem();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && jumping)
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

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower * 1000 * Time.deltaTime);
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        rb.velocity = Vector3.forward * verticalMove * moveSpeed * Time.deltaTime + new Vector3(0f, rb.velocity.y, 0f) + Vector3.right * horizontalMove * moveSpeed * Time.deltaTime;

        if (equippedItem != null && firearmEquipped)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mousePos.y;
            mousePos.y = transform.position.y;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            objectPos.z = objectPos.y;
            objectPos.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(mousePos - objectPos);
        }
        else if (Mathf.Abs(horizontalMove) > 0.01f || Mathf.Abs(verticalMove) > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation((transform.position + (verticalMove * Vector3.forward * moveSpeed + horizontalMove * Vector3.right * moveSpeed) - transform.position));
        }
    }

    private void UseItem()
    {
        InventoryManager.instance.UseItem(gameObject);

    }

    private void EquipItem()
    {
        equippedItem = InventoryManager.instance.EquipItem();
        if (equippedItem != null)
        {
            firearmEquipped = equippedItem.isFireArm;
            heldItem.GetComponent<MeshFilter>().mesh = equippedItem.itemMesh;
            GetComponent<Animator>().SetInteger("Item", equippedItem.idNum);

        }
        else
        {
            firearmEquipped = false;
            heldItem.GetComponent<MeshFilter>().mesh = null;
            GetComponent<Animator>().SetInteger("Item", -1);
        }
    }
    

}
