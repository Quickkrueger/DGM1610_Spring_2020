using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    private KeyCode[] konamiCode = {KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow,  KeyCode.DownArrow,  KeyCode.LeftArrow,  KeyCode.RightArrow,  KeyCode.LeftArrow,  KeyCode.RightArrow,  KeyCode.B,  KeyCode.A,  KeyCode.Return};
    private int currentCodePoint = 0;

    private void Update()
    {
        if (Input.GetKeyDown(konamiCode[currentCodePoint]))
        {
                currentCodePoint++;
        }
        else if (Input.anyKeyDown)
        {
            currentCodePoint = 0;
        }

        if(currentCodePoint >= 11)
        {
            SceneManager.LoadScene(2);
        }
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
