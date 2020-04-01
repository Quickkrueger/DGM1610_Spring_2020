using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int pointValue;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rabbit" || other.gameObject.tag == "Player")
        {
            ScoreManager.AddPoints(pointValue);
            PickupEffect();
            Destroy(gameObject);
        }
    }

    protected virtual void PickupEffect()
    {

    }
}
