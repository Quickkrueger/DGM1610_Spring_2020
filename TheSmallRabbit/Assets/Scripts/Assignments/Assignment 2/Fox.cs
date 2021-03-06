﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Predator
{

    private bool walking = false;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Wander());
    }

    protected override void Move()
    {
        if (walking)
        {
            base.Move();
            rb.velocity += new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    
    private IEnumerator Wander()
    {
        int timeToWander = Random.Range(1, 4);
        walking = Random.Range(0, 2) == 1;
        float randomRotation = Random.Range(-90f, 90f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + randomRotation, transform.eulerAngles.z);
        yield return new WaitForSeconds(timeToWander);
        StartCoroutine(Wander());
    }
}
