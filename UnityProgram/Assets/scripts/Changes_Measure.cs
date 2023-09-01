using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changes_Measure : MonoBehaviour
{
    public GameObject MeasurementActive, MeasureTool, CameraTransform;
    public InputField tool_pos_x, tool_pos_y, tool_pos_z;
    public GameObject[] MeasurementAxes;
    public Camera ChooseCam, ChooseCam1;
    private int a;

    void Start()
    {
        a = 0;
    }

    void Update()
    {
        if (MeasurementActive.activeSelf == true)
        {
            tool_pos_x.text = (MeasureTool.transform.localPosition.y * 1000).ToString();
            tool_pos_y.text = (MeasureTool.transform.localPosition.x * 1000).ToString();
            tool_pos_z.text = (MeasureTool.transform.localPosition.z * 1000).ToString();

            // control any object transformations (need to attatch to every object)
            if (CameraTransform.transform.hasChanged || Input.GetKey(KeyCode.Space) || a < 10)
            {
                ChooseCam.enabled = true;
                ChooseCam1.enabled = true;
                print("The transform has changed!");
                CameraTransform.transform.hasChanged = false;
                a++;
            }
            else
            {
                //  a = 0;
                ChooseCam.enabled = false;
                ChooseCam1.enabled = false;
            }
        }
    }

    // Reset measurements
    public void Reset_Measurements()
    {
        a = 0;
        tool_pos_x.text = "0";
        tool_pos_y.text = "0";
        tool_pos_z.text = "0";
        for (int i = 0; i < MeasurementAxes.Length; i++)
        {
            MeasurementAxes[i].transform.localPosition = new Vector3(0,0,0);
        }
    }

    // Measure Indication
    public void Measure_X()
    {
        tool_pos_x.image.color = Color.yellow;
    }
    public void Measure_Y()
    {
        tool_pos_y.image.color = Color.yellow;
    }
    public void Measure_Z()
    {
        tool_pos_z.image.color = Color.yellow;
    }
    public void Measure_Reset_color()
    {
        tool_pos_x.image.color = Color.white;
        tool_pos_y.image.color = Color.white;
        tool_pos_z.image.color = Color.white;
    }
}
