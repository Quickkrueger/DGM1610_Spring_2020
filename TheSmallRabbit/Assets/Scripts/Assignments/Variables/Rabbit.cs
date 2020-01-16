using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    // Rabbit data
    public string rabbitName;
    public int maxHealth;
    public int maxHunger;
    private int currentHealth;
    private int currentHunger;
    public Color coloration;
    // Start is called before the first frame update
    void Start()
    {
        InitializeRabbit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 200, gameObject.transform.position);
        }
    }

    void InitializeRabbit()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        GetComponent<Renderer>().material.color = coloration;
        Debug.Log("How much health we got? " + currentHealth);
    }
}
