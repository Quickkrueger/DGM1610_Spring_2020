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
    public GameObject player;
    void Start()
    {
        instance = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.value = player.GetComponent<Rabbit>().GetHunger();
        healthBar.value = player.GetComponent<Rabbit>().GetHealth();
    }

}
