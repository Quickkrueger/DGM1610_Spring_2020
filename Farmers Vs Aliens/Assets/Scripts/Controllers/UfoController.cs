using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UfoController : EnemyController
{
    public float speed;
    public float abductionSpeed;
    public float swayMultiplier;
    public GameObject abductionBeam;
    public GameObject glowingSphere;
    public Color Abducting;
    public Color Searching;
    public Color Abducted;
    private GameObject target;
    [Range(0.0f, 1.0f)]
    public float marginOfError;
    public float attackCooldown;
    private bool canAttack = true;
    public GameObject projectilePrefab;
    private bool timeToLeave = false;
    private bool hasPointer = false;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (!timeToLeave)
        {
            if (target != null && target.tag == "Cow")
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
            else if (target != null && target.tag == "Player" && SpawnManager._instance.CowsRemaining() > 0)
            {
                if (!LockedOnTarget(marginOfError + 5f))
                {
                    MoveTowardTarget();
                }
                else if(!LockedOnTarget(marginOfError + 1))
                {
                    MoveTowardTarget();
                    if (canAttack)
                    {
                        FireWeapon();
                    }
                }
                else
                {
                    if (canAttack)
                    {
                        FireWeapon();
                    }
                }
            }
            else
            {
                AcquireTarget();
                glowingSphere.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Searching);
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

    public bool LockedOnTarget(float distanceToLock)
    {
        if(transform.position.x < target.transform.position.x + distanceToLock && transform.position.x >= target.transform.position.x - distanceToLock &&
            transform.position.z < target.transform.position.z + distanceToLock && transform.position.z >= target.transform.position.z - distanceToLock)
        {
            return true;
        }
        return false;
    }

    public bool LockedOnTarget()
    {
        if (target != null && transform.position.x < target.transform.position.x + marginOfError && transform.position.x >= target.transform.position.x - marginOfError &&
            transform.position.z < target.transform.position.z + marginOfError && transform.position.z >= target.transform.position.z - marginOfError)
        {
            return true;
        }
        return false;
    }

    private void Abduct()
    {
        if (!hasPointer)
        {
            GameManager._instance.CreateUFOPointer(gameObject);
            hasPointer = true;
        }
        abductionBeam.SetActive(true);
        glowingSphere.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Abducting);
        transform.GetChild(0).GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        target.GetComponent<CowController>().enabled = false;
        target.GetComponent<NavMeshAgent>().enabled = false;
        target.transform.position += transform.up * Time.deltaTime * abductionSpeed;
        float uniformScale = Vector3.Distance(transform.position, target.transform.position) / (transform.position.y - 0.75f);
        target.transform.localScale = new Vector3(uniformScale, uniformScale, uniformScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cow")
        {
            SpawnManager._instance.DestroyCow(other.gameObject);
            target = null;
            UIManager._instance.UpdateCowCounter();
            abductionBeam.SetActive(false);
            transform.GetChild(0).GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }

    private void AcquireTarget()
    {
        GameObject[] potentialTargets = SpawnManager._instance.ProvideTargetList();
        hasPointer = false;

        if (potentialTargets.Length > 1)
        {
            target = potentialTargets[Random.Range(0, potentialTargets.Length)];
            target.GetComponent<CowController>().Claim();
        }
        else if(potentialTargets.Length == 1)
        {
            target = potentialTargets[0];
        }
        else
        {
            if (SpawnManager._instance.CowsRemaining() > 0)
            {
                target = GameManager._instance.GetPlayer();
                glowingSphere.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Abducted);
            }
            else
            {
                timeToLeave = true;
            }
        }

        if(target != null && target.tag == "Cow")
        {
            target.GetComponent<CowController>().Claim();
        }
    }

    private void Leave()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    protected override void Explode()
    {
        if (target != null && target.tag == "Cow")
        {

            target.transform.localScale = new Vector3(1f, 1f, 1f);
            target.transform.position = new Vector3(target.transform.position.x, 0.75f, target.transform.position.z);
            target.GetComponent<CowController>().enabled = true;
            target.GetComponent<NavMeshAgent>().enabled = true;
            target.GetComponent<CowController>().UnClaim();
        }
        base.Explode();
    }

    private void FireWeapon()
    {
        canAttack = false;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.transform.LookAt(target.transform.position);
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
