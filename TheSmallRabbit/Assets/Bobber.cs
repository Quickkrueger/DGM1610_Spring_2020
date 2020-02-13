using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    bool recalled = false;
    private float recallSpeed = 50f;
    GameObject owner;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") <= 0.01f)
        {
            recalled = true;
            GetComponent<SphereCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (recalled)
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
        if (recalled)
        {
            Destroy(gameObject);
        }
    }

    public void SetOwner(GameObject player)
    {
        owner = player;
    }
}
