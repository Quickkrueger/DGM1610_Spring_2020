using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PressStart()
    {
        SceneManager.LoadScene(1);
    }

    public void PressQuit()
    {
        Application.Quit();
    }
}
