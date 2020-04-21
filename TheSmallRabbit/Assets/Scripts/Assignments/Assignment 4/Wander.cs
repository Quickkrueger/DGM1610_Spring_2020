using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{

    public float wanderRadius;
    public float wanderTimer;
    public float speed;
    private float currentSpeed;
    public float alertDist;
    public float attackDist;

    private GameObject target;
    private NavMeshAgent agent;
    private float timer;
    private float distance;
    private Vector3 direction;

    public int damage;

    // Start is called before the first frame update
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    private void Start()
    {
        currentSpeed = speed;
        target = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(target.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < alertDist && distance > attackDist)
        {
            currentSpeed += 2 * Time.deltaTime;
            transform.LookAt(target.transform);
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        else if (distance <= attackDist)
        {
            direction = target.transform.position - transform.position;
            direction.y = 0;
            currentSpeed += 5 * Time.deltaTime;
            transform.LookAt(target.transform);
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            if(direction.magnitude <= attackDist)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
        else if (distance > alertDist)
        {
            currentSpeed = speed;
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, 10);
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
