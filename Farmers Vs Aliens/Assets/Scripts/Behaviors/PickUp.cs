﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            GetComponent<Rigidbody>().useGravity = false;
            Collider[] colliders = GetComponentsInChildren<Collider>();
            for(int i = 0; i < colliders.Length; i++)
            {
                colliders[i].isTrigger = true;
            }
        }

        if(other.tag == "Player")
        {
            PickUpEffect();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpEffect();
        }
    }

    protected virtual void PickUpEffect()
    {
        Destroy(gameObject);
    }
}
