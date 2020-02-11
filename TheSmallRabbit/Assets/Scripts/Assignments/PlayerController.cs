using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
        private bool jumping = false;
        public float moveSpeed;
        public float rotateSpeed = 2;
        public GameObject projectilePrefab;

        // Start is called before the first frame update
        void Start()
        {

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
                Fire();
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
        if (Mathf.Abs(horizontalMove) > 0.01f || Mathf.Abs(verticalMove) > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation((transform.position + (verticalMove * Vector3.forward * moveSpeed + horizontalMove * Vector3.right * moveSpeed) - transform.position));
        }
        }

        private void Fire()
        {
            Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        }
    }
