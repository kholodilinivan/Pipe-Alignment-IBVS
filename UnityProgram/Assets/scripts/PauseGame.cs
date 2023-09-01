using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public Camera ChooseCam, ChooseCam1;
    private int a;

    void Start()
    {
        a = 0;   
    }

    void Update()
    {
        // control any object transformations (need to attatch to every object)
        if (transform.hasChanged || Input.GetKey(KeyCode.Space) || a < 10)
        {
            ChooseCam.enabled = true;
            ChooseCam1.enabled = true;
            print("The transform has changed!");
            transform.hasChanged = false;
            a++;
        }
        else
        {
            //  a = 0;
            ChooseCam.enabled = false;
            ChooseCam1.enabled = false;
        }
    }

    public void ClickPause()
    {
        if (Time.timeScale == 1) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    private void OnMouseDown()
    {
        
    }
}
