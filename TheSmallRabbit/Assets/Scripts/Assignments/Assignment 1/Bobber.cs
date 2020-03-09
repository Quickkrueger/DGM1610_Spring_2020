using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : Projectile
{
    bool recalled = false;
    private float recallSpeed = 50f;
    // Update is called once per frame

    private void Start()
    {
        GetComponent<Rigidbody>().velocity += (transform.forward + transform.up) * 5;
    }
    void Update()
    {
        if (Input.GetAxis("Fire1") <= 0.01f && !recalled)
        {
            recalled = true;
            GetComponent<SphereCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (recalled)
        {
            ReturnToPlayer();
        }
    }

    private void ReturnToPlayer()
    {
        transform.LookAt(owner.transform);
        transform.Translate(Vector3.forward * recallSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (recalled && other.gameObject == owner)
        {
            InventoryManager.instance.CoolDown();
            Destroy(gameObject);
        }
    }

    
}
