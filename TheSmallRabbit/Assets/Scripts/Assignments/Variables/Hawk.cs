using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : MonoBehaviour
{
    private int attackDamage;
    private int sightRadius;
    public int speed;
    public Color coloration;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        sightRadius = 20;
        GetComponent<Renderer>().material.color = coloration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
