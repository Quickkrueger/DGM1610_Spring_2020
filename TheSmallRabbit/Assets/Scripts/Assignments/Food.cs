using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Pickup
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

    void Update()
    {
        if (!spoiled)
        {
            spoiled = CheckSpoiled();
        }
    }

    // Update is called once per frame
    protected override void PickupEffect()
    {
        base.PickupEffect();
        GameManager.instance.FeedRabbit(5);
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
