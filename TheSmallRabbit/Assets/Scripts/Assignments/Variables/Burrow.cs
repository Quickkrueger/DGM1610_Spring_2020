using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burrow : MonoBehaviour
{
    bool occupied = false;
    Rabbit occupant;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AllowOccupant(Rabbit requester)
    {
        if (occupied)
        {
            return false;
        }
        occupant = requester;
        occupied = true;

        return true;
    }

    public void EjectOccupant()
    {
            occupant = null;
            occupied = false;
    }


}
