using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject BtnSimulation, BtnCalib, BtnMeasurement, walleMotiont, 
    PanelSimulation, PanelSimulation1, PanelCalib, PanelCalib1, PanelMeasure,
    PanelMeasure1, PanelPause, GO_Simulation, GO_Calib, GO_Measure,
    BtnReset, BtnQuit;
    bool isPaused = false;

    private int a,b;

    public GameObject[] myToggle;

    void Start()
    {
        a = 0;
        b = 0;
        BtnSimulation.SetActive(false);
        BtnMeasurement.SetActive(true);
        BtnCalib.SetActive(true);
        BtnReset.SetActive(true);
        BtnQuit.SetActive(true);
        PanelSimulation.SetActive(true);
        PanelSimulation1.SetActive(true);
        PanelCalib.SetActive(false);
        PanelCalib1.SetActive(false);
        PanelMeasure.SetActive(false);
        PanelMeasure1.SetActive(true);
        PanelPause.SetActive(false);
        GO_Simulation.SetActive(true);
        GO_Calib.SetActive(false);
        GO_Measure.SetActive(false);
    }

    void Update()
    {
        if (b == 0)
        {
            if (walleMotiont.GetComponent<walle>().enabled == true)
            {
                a = 1;
            }
            else a = 0;    
        }
    }

    public void ClickCalib()
    {
        b = 1;
        if (a == 1)
        {
            walleMotiont.GetComponent<walle>().enabled = false;   
        }
        BtnSimulation.SetActive(true);
        BtnMeasurement.SetActive(false);
        BtnCalib.SetActive(false);
        BtnReset.SetActive(false);
        BtnQuit.SetActive(false);
        PanelSimulation.SetActive(false);
        PanelSimulation1.SetActive(false);
        PanelCalib.SetActive(true);
        PanelCalib1.SetActive(true);
        PanelMeasure1.SetActive(false);
        PanelMeasure.SetActive(false);
        GO_Simulation.SetActive(false);
        GO_Calib.SetActive(true);
        GO_Measure.SetActive(false);

        for (int i = 0; i < myToggle.Length; i++)
        {
            myToggle[i].GetComponent<Toggle>().isOn = false;
        }
        a = 0;
    }

    public void ClickSimulation()
    {
        PanelMeasure1.SetActive(false);
        StartCoroutine(ExampleCoroutine());
        
    }

    public void ClickMeasure()
    {
        b = 1;
        if (a == 1)
        {
            walleMotiont.GetComponent<walle>().enabled = false;
        }
        BtnSimulation.SetActive(true);
        BtnMeasurement.SetActive(true);
        BtnCalib.SetActive(false);
        BtnReset.SetActive(false);
        BtnQuit.SetActive(false);
        PanelSimulation.SetActive(false);
        PanelSimulation1.SetActive(false);
        PanelCalib.SetActive(false);
        PanelCalib1.SetActive(false);
        PanelMeasure1.SetActive(true);
        PanelMeasure.SetActive(true);
        GO_Simulation.SetActive(false);
        GO_Calib.SetActive(false);
        GO_Measure.SetActive(true);
        GO_Measure.transform.parent = GameObject.Find("walle3").transform;
    }

    public void ResetScene()
    {
        if (GameObject.Find("Server").GetComponent<Server_new>().enabled == true)
        {
               GameObject server = GameObject.Find("Server");
               Server_new serverIns = server.GetComponent<Server_new>();
               serverIns.OnApplicationQuit();
            //   serverIns.server.Stop();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            GameObject.Find("ButtonPause").gameObject.GetComponentInChildren<Text>().text = "Pause";
            PanelPause.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            GameObject.Find("ButtonPause").gameObject.GetComponentInChildren<Text>().text = "Play";
            PanelPause.SetActive(true);
        }
    }

    public void OpenWebSite()
    {
        Application.OpenURL("https://www.ilabit.org");
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1/10);

        b = 0;
        //if (a == 1)
        //{
            walleMotiont.GetComponent<walle>().enabled = true;
        // }
        BtnSimulation.SetActive(false);
        BtnMeasurement.SetActive(true);
        BtnCalib.SetActive(true);
        BtnReset.SetActive(true);
        BtnQuit.SetActive(true);
        PanelSimulation.SetActive(true);
        PanelSimulation1.SetActive(true);
        PanelCalib.SetActive(false);
        PanelCalib1.SetActive(false);
        PanelMeasure.SetActive(false);
        GO_Simulation.SetActive(true);
        GO_Calib.SetActive(false);
        GO_Measure.SetActive(false);
        GO_Measure.transform.parent = GameObject.Find("walle").transform;
    }

}
