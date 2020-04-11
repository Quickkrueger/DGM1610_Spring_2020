using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UfoController : MonoBehaviour
{
    public float speed;
    public float abductionSpeed;
    public float swayMultiplier = 0.05f;
    public GameObject abductionBeam;
    private GameObject target;
    [Range(0.0f, 1.0f)]
    public float marginOfError;
    private bool timeToLeave = false;


    // Start is called before the first frame update
    void Start()
    {
        AcquireTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!timeToLeave)
        {
            if (target != null)
            {
                if (!LockedOnTarget())
                {
                    MoveTowardTarget();
                }
                else
                {
                    Abduct();
                }
            }
            else
            {
                AcquireTarget();
            }
        }
        else
        {
            Leave();
        }
    }

    private void MoveTowardTarget()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        transform.position += transform.forward * speed * Time.deltaTime; ;
        transform.position += transform.right * Mathf.Sin(Time.time * 3.14f) * swayMultiplier;
    }

    private bool LockedOnTarget()
    {
        if(transform.position.x < target.transform.position.x + marginOfError && transform.position.x >= target.transform.position.x - marginOfError &&
            transform.position.z < target.transform.position.z + marginOfError && transform.position.z >= target.transform.position.z - marginOfError)
        {
            return true;
        }
        return false;
    }

    private void Abduct()
    {

        abductionBeam.SetActive(true);
        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        target.GetComponent<CowController>().enabled = false;
        target.GetComponent<NavMeshAgent>().enabled = false;
        target.transform.position += transform.up * Time.deltaTime * abductionSpeed;
        float uniformScale = Vector3.Distance(transform.position, target.transform.position) / (transform.position.y - 0.75f);
        target.transform.localScale = new Vector3(uniformScale, uniformScale, uniformScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cow")
        {
            Destroy(other.gameObject);
            abductionBeam.SetActive(false);
            GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }

    private void AcquireTarget()
    {
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("Cow");
        if (potentialTargets.Length > 1)
        {
            target = potentialTargets[Random.Range(0, potentialTargets.Length)];
        }
        else if(potentialTargets.Length == 1)
        {
            target = potentialTargets[0];
        }
        else
        {
            timeToLeave = true;
        }
    }

    private void Leave()
    {
        transform.position += transform.up;
    }


}
