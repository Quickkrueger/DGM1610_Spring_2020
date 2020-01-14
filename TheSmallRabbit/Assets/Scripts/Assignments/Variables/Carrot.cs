using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    Color color;
    bool spoiled;
    int timeUntilSpoiled;
    // Start is called before the first frame update
    void Start()
    {
        spoiled = false;
        timeUntilSpoiled = 2000;
        color = Color.red + Color.yellow;
        GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpoiled--;
        if (timeUntilSpoiled == 0)
        {
            color = color = new Color(0.59f, 0.29f, 0f);
            GetComponent<Renderer>().material.color = color;
        }
    }
}
