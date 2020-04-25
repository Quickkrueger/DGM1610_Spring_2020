using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager _instance;
    private GameObject player;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnManager._instance.NoMoreCows() || player.GetComponent<PlayerController>().IsDead())
        {
            GameOver();
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
    }

    public void GameOver()
    {

    }
}
