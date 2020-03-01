using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public Slider hungerBar;
    public Slider healthBar;
    public CanvasGroup gameOverScreen;
    public int maxHealth;
    public int maxHunger;
    private int currentHealth;
    private int currentHunger;
    private float timeInterval = 5;
    public GameObject player;
    void Start()
    {
        gameOverScreen.alpha = 0;
        instance = GetComponent<GameManager>();

        hungerBar.maxValue = maxHunger;
        healthBar.maxValue = maxHealth;

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

    public void FeedRabbit(int satiation)
    {
        currentHunger += satiation;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    public void HarmRabbit(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        StartCoroutine(FadeInGameOver());
    }

    IEnumerator FadeInGameOver()
    {
        while(gameOverScreen.alpha < 1)
        {
            yield return new WaitForSeconds(0.01f);
            gameOverScreen.alpha += 0.01f;
        }
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
