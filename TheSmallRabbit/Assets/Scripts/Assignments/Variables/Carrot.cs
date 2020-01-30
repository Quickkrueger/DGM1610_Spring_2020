using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public Color color;
    public Color spoiledColor;
    bool spoiled;
    public int timeUntilSpoiled = 1000;
    // Start is called before the first frame update
    void Start()
    {
        spoiled = false;
        GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!spoiled)
        //{
        //    spoiled = CheckSpoiled();
        //}
    }

    bool CheckSpoiled()
    {
        if (timeUntilSpoiled > 0)
        {
            timeUntilSpoiled--;
        }
        else if (timeUntilSpoiled == 0)
        {
            GetComponent<Renderer>().material.color = spoiledColor;

            return true;
        }
        return false;
    }
}
