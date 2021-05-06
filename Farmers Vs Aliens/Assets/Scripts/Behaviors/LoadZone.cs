using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZone : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform destination;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Transform parent = other.transform.parent;
        if (parent != null)
        {
            other.transform.parent.position = destination.position;
        }
        else
        {
            other.transform.position = destination.position;
        }
    }


}
