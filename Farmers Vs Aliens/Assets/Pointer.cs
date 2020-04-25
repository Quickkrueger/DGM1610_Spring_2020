using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    private GameObject target;
    bool initialized = false;

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.GetComponent<UfoController>().LockedOnTarget(0))
        {
            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
        }
        else if (initialized)
        {
            Destroy(gameObject);
        }
    }

    public void InitializePointer(GameObject ufo)
    {
        target = ufo;
        initialized = true;
    }

    public void RemovePointer()
    {
        Destroy(gameObject);
    }
}
