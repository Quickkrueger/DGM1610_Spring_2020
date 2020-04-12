using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsAndSwitch : MonoBehaviour
{

    enum Days {Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday=10};
    Days currentDay;
    public string favHero;
    // Start is called before the first frame update
    void Start()
    {
        currentDay = Days.Wednesday;
        switch (favHero)
        {
            case "Superman":
                print("Wrong");
                break;
            case "Thor":
                print("Correct");
                break;
            default:
                print("Isn't a real hero");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDay == Days.Monday)
        {
            print("It is Monday.");
        }
    }
}
