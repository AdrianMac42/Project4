using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{

    public void pause()
    {
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
    }

    public void unpause()
    {
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
    }

    public void speed()
    {
        if (Time.timeScale < 10)
        {
            Time.timeScale += 2;
            Debug.Log(Time.timeScale);
        }
    }

    public void slow()
    {
        if (Time.timeScale > 2)
        {
            Time.timeScale -= 2;
            Debug.Log(Time.timeScale);
        }
        else if (Time.timeScale == 2)
        {
            Time.timeScale -= 1;
            Debug.Log(Time.timeScale);
        }
    }
}
