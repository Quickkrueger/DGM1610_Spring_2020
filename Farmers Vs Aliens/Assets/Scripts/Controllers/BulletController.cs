using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private bool randomTragectoryVariation;
    private float randomTrajectoryVariationMagnitude;
    private float speed;
    private int damage;
    private float xDeviation;
    private float yDeviation;
    private const float DESTROY_TIME = 2f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DESTROY_TIME);
        InitializeBullet();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += ((transform.up * yDeviation) + (transform.right * xDeviation) + transform.forward) * speed * Time.deltaTime;
    }

    public void InitializeBullet(bool hasSpread, float magnitude, float bulletSpeed)
    {
        speed = bulletSpeed;
        randomTragectoryVariation = hasSpread;
        randomTrajectoryVariationMagnitude = magnitude;

        if (randomTragectoryVariation)
        {
            xDeviation = Random.Range(randomTrajectoryVariationMagnitude * -1, randomTrajectoryVariationMagnitude);
            xDeviation = Random.Range(randomTrajectoryVariationMagnitude * -1, randomTrajectoryVariationMagnitude);
        }
        else
        {
            xDeviation = 0;
            yDeviation = 0;
        }
    }
    void InitializeBullet()
    {
        damage = 1;
        if (randomTragectoryVariation)
        {
            xDeviation = Random.Range(randomTrajectoryVariationMagnitude * -1, randomTrajectoryVariationMagnitude);
            yDeviation = Random.Range(randomTrajectoryVariationMagnitude * -1, randomTrajectoryVariationMagnitude);
        }
        else
        {
            xDeviation = 0;
            yDeviation = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponentInParent<UfoController>().TakeDamage(damage);
        }
    }
}
