using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    // Rabbit data
    string rabbitName;
    int maxHealth;
    int maxHunger = 20;
    int currentHealth;
    int currentHunger;
    Color coloration;
    // Start is called before the first frame update
    void Start()
    {
        rabbitName = "Peter";
        maxHealth = 10;
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        coloration = Color.black;
        GetComponent<Renderer>().material.color = coloration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 200, gameObject.transform.position);
        }
    }
}
