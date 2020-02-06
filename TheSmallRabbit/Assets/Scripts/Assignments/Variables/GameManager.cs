using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public Slider hungerBar;
    public Slider healthBar;
    public int maxHealth;
    public int maxHunger;
    private int currentHealth;
    private int currentHunger;
    private float timeInterval = 5;
    public GameObject player;
    void Start()
    {
        instance = GetComponent<GameManager>();
        currentHealth = maxHealth;
        currentHunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.value = currentHunger;
        healthBar.value = currentHealth;
    }

    private void FixedUpdate()
    {
        timeInterval = timeInterval - Time.deltaTime;
        if (timeInterval <= 0)
        {
            currentHunger--;
            timeInterval = 5;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        }
    }

    public void FeedRabbit()
    {
        currentHunger += 5;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

}
