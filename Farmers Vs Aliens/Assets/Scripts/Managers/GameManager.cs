using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager _instance;
    private GameObject player;
    private GameObject shopkeeper;
    public GameObject pointerPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            _instance = null;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shopkeeper = GameObject.FindGameObjectWithTag("Shopkeeper");
        SoundController._instance.PlayChillMusic();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)){
                UIManager._instance.TogglePauseScreen();
        }

        if (SpawnManager._instance.NoMoreCows())
        {
            GameOver(0);
        }
        else if (player.GetComponent<PlayerController>().IsDead())
        {
            GameOver(1);
        }

        if (SpawnManager._instance.NoMoreWaves())
        {
            Victory();
        }
        else if (SpawnManager._instance.WaveFinished() && UIManager._instance.WaveTextStatus() == false)
        {
            UIManager._instance.EnableWaveText();
            SoundController._instance.PlayChillMusic();
            AnimationManager._instance.RoundEnd();
            shopkeeper.GetComponent<Merchant>().SetupShop();

        }
        else if(UIManager._instance.WaveTextStatus() == true && Input.GetButtonDown("P1 Submit"))
        {
            UIManager._instance.DisableWaveText();
            SpawnManager._instance.StartWave();
            SoundController._instance.PlayIntensemusic();
            AnimationManager._instance.RoundStart();
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void CreateUFOPointer(GameObject ufo)
    {
        GameObject pointer = Instantiate(pointerPrefab, player.transform.position, player.transform.rotation);
        pointer.transform.parent = player.transform;
        pointer.GetComponent<Pointer>().InitializePointer(ufo);
    }
    

    public void GameOver(int type)
    {
        UIManager._instance.EnableGameOverScreen(type);
        player.SetActive(false);
    }

    public void Victory()
    {
        UIManager._instance.EnableVictoryScreen();
        player.SetActive(false);
    }
}
