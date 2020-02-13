using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jumping = false;
    public float moveSpeed;
    public float rotateSpeed = 2;
    public GameObject projectilePrefab;
    private ItemScriptableObject equippedItem;
    private bool firearmEquipped = false;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0.01f && !jumping)
        {
            Jump();
        }

        Move();

        if (Input.GetAxis("Fire1") > 0.01f)
        {
            UseItem();
        }

        if (Input.GetAxis("Use") > 0.01f)
        {
            //Interact();   
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventoryManager.instance.ItemToEquip(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.instance.ItemToEquip(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.instance.ItemToEquip(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InventoryManager.instance.ItemToEquip(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            InventoryManager.instance.ItemToEquip(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            InventoryManager.instance.ItemToEquip(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            InventoryManager.instance.ItemToEquip(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            InventoryManager.instance.ItemToEquip(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            InventoryManager.instance.ItemToEquip(9);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            InventoryManager.instance.ItemToEquip(0);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && jumping)
        {
            jumping = false;
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

        GetComponent<Rigidbody>().velocity = Vector3.forward * verticalMove * moveSpeed * Time.deltaTime + new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f) + Vector3.right * horizontalMove * moveSpeed * Time.deltaTime;

        if (equippedItem != null && equippedItem.ID == "slingshot")
        {
            firearmEquipped = true;
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
        if (firearmEquipped)
        {
            Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        }
    }

}
