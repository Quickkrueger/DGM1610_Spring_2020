using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : Enemy
{

    protected override void Start()
    {
        base.Start();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
    }
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
