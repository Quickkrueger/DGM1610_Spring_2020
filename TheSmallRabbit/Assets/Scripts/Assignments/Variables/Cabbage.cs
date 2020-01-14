using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : MonoBehaviour
{
    Color color;
    bool spoiled;
    int timeUntilSpoiled;
    // Start is called before the first frame update
    void Start()
    {
        spoiled = false;
        timeUntilSpoiled = 1000;
        color = Color.green;
        GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpoiled--;
        if(timeUntilSpoiled == 0)
        {
            color = Color.yellow;
            GetComponent<Renderer>().material.color = color;
        }
    }
}
