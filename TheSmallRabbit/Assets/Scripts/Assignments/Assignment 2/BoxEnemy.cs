using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : Enemy
{

    // Update is called once per frame
    void Update()
    {

        TargetPlayer();
    }


    protected void TargetPlayer()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
}
