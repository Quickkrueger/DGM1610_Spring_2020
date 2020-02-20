using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jumping = false;
    public float moveSpeed;
    public float rotateSpeed = 2;
    public GameObject projectilePrefab;
    public GameObject bobberPrefab;
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

        if (Input.GetAxis("Fire1") > 0.01f && equippedItem != null)
        {
            //TODO: Move item prefab tracking and behaviors to inventorymanager/ unique item scripts, including the "canFire" functionality.
            UseItem();
        }

        if (Input.GetAxis("Use") > 0.01f)
        {
            //Interact();   
        }

        EquipItem();

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
        }
        else
        {
            firearmEquipped = false;
        }
    }
    

}
