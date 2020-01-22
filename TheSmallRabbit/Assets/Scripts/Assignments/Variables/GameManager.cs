using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    private int score = 0;
    public Text scoreText;
    public Slider hungerBar;
    public Slider healthBar;
    void Start()
    {
        instance = GetComponent<GameManager>();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
