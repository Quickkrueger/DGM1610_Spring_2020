using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public Text numCows;
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCowCounter()
    {

        numCows.text = SpawnManager._instance.CowsRemaining().ToString();
    }

}
