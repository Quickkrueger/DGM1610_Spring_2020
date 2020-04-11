using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowController : MonoBehaviour
{
    public float wanderRadius;
    private float actionTimer;
    
    private NavMeshAgent agent;
    private float timer = 0;
    private bool wandering = false;
    private bool actionInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!actionInProgress)
        {
            int wander = Random.Range(0, 5);
            wandering = wander == 4;
            actionTimer = Random.Range(5.0f, 10.0f);
        }

        timer += Time.deltaTime;

        if (wandering)
        {

            if (timer >= actionTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
        else
        {
            if (timer >= actionTimer)
            {
                timer = 0;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layerMask);

        return navHit.position;
    }
}
