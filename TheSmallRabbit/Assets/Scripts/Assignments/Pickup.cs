using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rabbit")
        {
            PickupEffect();
            Destroy(gameObject);
        }
    }

    protected virtual void PickupEffect()
    {

    }
}
