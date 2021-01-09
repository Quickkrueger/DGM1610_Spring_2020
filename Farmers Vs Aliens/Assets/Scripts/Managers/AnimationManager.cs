using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator barnAnimator;
    public static AnimationManager _instance;
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
    void Start()
    {
        
    }

    public void RoundStart()
    {
        barnAnimator.SetBool("Open", false);
    }

    public void RoundEnd()
    {
        barnAnimator.SetBool("Open", true);
    }

}
