using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public Text numCows;

    public Slider healthBar;

    public Text numMoney;

    public Text nextWave;

    public GameObject victoryScreen;
    public GameObject gameoverScreen;
    public Text[] gameoverText;
    // Start is called before the first frame update
    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void UpdateCowCounter()
    {

        numCows.text = SpawnManager._instance.CowsRemaining().ToString();
    }

    public void InitializeHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthBar.value = health;
    }

    public void SetMoney(int money)
    {
        numMoney.text = money.ToString();
    }

    public void DisableWaveText()
    {
        nextWave.enabled = false;
    }

    public void EnableWaveText()
    {
        nextWave.enabled = true;
    }

    public bool WaveTextStatus()
    {
        return nextWave.enabled;
    }

    public void EnableGameOverScreen(int type)
    {
        gameoverScreen.SetActive(true);
        gameoverText[type].enabled = true;
    }

    public void EnableVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }
}
