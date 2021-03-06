﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    static int totalPlayerNum = 0;

    public float moveSpeed;
    public float jumpPower;
    public Color[] hatColors;
    public GameObject item;
    public GameObject bulletPrefab;
    public WeaponScriptableObject currentWeapon;
    public int maxHealth;

    private int currentHealth;
    private int money = 10;
    private int thisPlayerNum;
    private bool grounded = true;
    private bool canFire = true;
    private bool usingMouse;
    private Rigidbody rb;

    void Start()
    {

        totalPlayerNum++;
        thisPlayerNum = totalPlayerNum;
        currentHealth = maxHealth;

        if (thisPlayerNum == 1)
        {
            usingMouse = true;
        }
        else
        {
            usingMouse = false;
        }

        rb = GetComponent<Rigidbody>();
        transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", hatColors[thisPlayerNum - 1]);
        UIManager._instance.InitializeHealth(maxHealth);
        UIManager._instance.SetMoney(money);
    }

    

    private void Update()
    {
        //if (Input.GetButtonDown("P" + thisPlayerNum + " Jump") && grounded)
        //{
        //    Jump();
        //}

        if (currentWeapon != null)
        {
            if (item.transform.childCount <= 0)
            {
                GameObject newItem = Instantiate(currentWeapon.model);
                newItem.transform.parent = item.transform;
                newItem.transform.position = item.transform.position;
                newItem.transform.localRotation = currentWeapon.model.transform.rotation;

                GetComponent<AudioSource>().clip = currentWeapon.gunSound;
            }

            LookForTarget();
        }

        if (Input.GetButton("P" + thisPlayerNum + " Fire1") && currentWeapon != null && canFire)
        {
            Fire();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower * 1000 * Time.deltaTime);
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("P" + thisPlayerNum + " Horizontal");
        float verticalMove = Input.GetAxis("P" + thisPlayerNum + " Vertical");

        rb.velocity = Vector3.forward * verticalMove * moveSpeed * Time.deltaTime + new Vector3(0f, rb.velocity.y, 0f) + Vector3.right * horizontalMove * moveSpeed * Time.deltaTime;

        if(thisPlayerNum == 1 && (Mathf.Abs(Input.GetAxis("P1 Mouse X")) >= 0.01f || Mathf.Abs(Input.GetAxis("P1 Mouse Y")) >= 0.01f))
        {
            usingMouse = true;
        }
        else if ((Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick X")) >= 0.05f || Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick Y")) >= 0.05f))
        {
            usingMouse = false;
        }

        FindAndLookatTarget();
        
    }

    private void FindAndLookatTarget()
    {
        if (usingMouse)
        {
            FindTargetWithMouse();
        }
        else if (!usingMouse && (Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick X")) >= 0.05f || Mathf.Abs(Input.GetAxis("P" + thisPlayerNum + " Joystick Y")) >= 0.05f))
        {
            
        }

    }

    private void FindTargetWithMouse()
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject rayhit = hit.collider.gameObject;
            if (rayhit.tag.Equals("Enemy"))
            {
                PointWeapon(hit.collider.gameObject.transform);

                Vector3 targetPos = rayhit.transform.position;
                targetPos.y = transform.position.y;
                //mousePos.y = 0;

                
                transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
                //transform.LookAt(mousePos - objectPos);

            }
            else
            {
                item.transform.rotation = gameObject.transform.rotation;

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = mousePos.y;
                mousePos.y = transform.position.y;
                //mousePos.y = 0;

                Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
                objectPos.z = objectPos.y;
                objectPos.y = transform.position.y;
                transform.rotation = Quaternion.LookRotation(mousePos - objectPos);
                //transform.LookAt(mousePos - objectPos);
            }
        }
    }

    private void FindTargetWithController()
    {
        Vector3 joystickPos = new Vector3(Input.GetAxisRaw("P" + thisPlayerNum + " Joystick X"), 0f, Input.GetAxisRaw("P" + thisPlayerNum + " Joystick Y"));
        transform.LookAt(joystickPos + transform.position);

        LookForClosestValidTarget();
    }

    private void LookForClosestValidTarget()
    {
        Vector3 lookAngle = transform.rotation.eulerAngles;


    }

    private void LookForTarget()
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Equals("Enemy"))
            {
                PointWeapon(hit.collider.gameObject.transform);
            }
            else
            {
                item.transform.rotation = gameObject.transform.rotation;
            }
        }

    }

    private void PointWeapon(Transform target)
    {
        item.transform.LookAt(target);
    }

    private void Fire()
    {
        canFire = false;
        GetComponent<AudioSource>().Play();
        for(int i = 0; i < currentWeapon.numProjectile; i++)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, item.transform.position + item.transform.forward, item.transform.rotation);
            currentBullet.GetComponent<BulletController>().InitializeBullet(currentWeapon.hasSpread, currentWeapon.spreadRange, currentWeapon.projectileSpeed);
        }
        StartCoroutine(Cooldown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" && !grounded)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && grounded)
        {
            grounded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        ChangeHealth(damage * -1);
    }

    public void GainHealth(int health)
    {
        ChangeHealth(health);
    }

    private void ChangeHealth(int change)
    {
        currentHealth += change;
        currentHealth =  Mathf.Clamp(currentHealth, 0, maxHealth);
        UIManager._instance.SetHealth(currentHealth);
    }

    public void GainMoney(int money)
    {
        ChangeMoney(money);
    }

    public bool LoseMoney(int money)
    {
        if (this.money > money)
        {
            ChangeMoney(money * -1);
            return true;
        }
        return false;
    }

    private void ChangeMoney(int change)
    {
        money += change;
        money = Mathf.Clamp(money, 0, 10000000);
        UIManager._instance.SetMoney(money);
    }

    public bool IsDead()
    {
        if(currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(currentWeapon.cooldownTime);
        canFire = true;
    }

    public void SwapWeapon(WeaponScriptableObject weapon)
    {
        Destroy(item.transform.GetChild(0).gameObject);
        currentWeapon = weapon;
    }

    public static void ResetPlayerCount()
    {
        totalPlayerNum = 0;
    }
}
