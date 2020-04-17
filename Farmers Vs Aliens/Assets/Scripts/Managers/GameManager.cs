using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager _instance;
    private GameObject player;
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
        
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
