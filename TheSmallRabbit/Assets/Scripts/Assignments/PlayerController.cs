using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jumping = false;
    public float moveSpeed;
    public float rotateSpeed = 2;
    private List<ItemScriptableObject> items;
    public GameObject projectilePrefab;
    private GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<ItemScriptableObject>();
    }

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
            Interact();   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && jumping)
        {
            jumping = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item" && item == null)
        {
            item = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            item = null;
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

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mousePos.y;
        mousePos.y = transform.position.y;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        objectPos.z = objectPos.y;
        objectPos.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(mousePos - objectPos);
    }

    private void UseItem()
    {
        Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
    }

    private void Interact()
    {
        if(item != null)
        {
            items.Add(item.GetComponent<FishingRod>().fishingRodData);
            Destroy(item);
        }
    }

}
