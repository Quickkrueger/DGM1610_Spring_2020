using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rabbit" || other.gameObject.tag == "Player")
        {
            PickupEffect();
            Destroy(gameObject);
        }
    }

    protected virtual void PickupEffect()
    {

    }
}
