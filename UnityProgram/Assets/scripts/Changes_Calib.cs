using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Changes_Calib : MonoBehaviour
{
    public GameObject[] FishLow, FishHigh, FishCapture, Cam_control;
    public GameObject DroneConfig_1, Cam_manual_text;

    // Camera Config
    public GameObject[] FOV_1, CamFast_1, CamSimple_1, CamGood_1, CamBeauty_1, CamFantast_1;
    public Dropdown camFOV_1, camQuality_1;
    public InputField cam_pos_x, cam_pos_y, cam_pos_z,
        cam_pos_x_manual, cam_pos_y_manual, cam_pos_z_manual,
        cam_rot_x, cam_rot_y, cam_rot_z,
        cam_rot_x_manual, cam_rot_y_manual, cam_rot_z_manual;

    // Checkerboards Config
    public GameObject Chess;
    public InputField chess_pos_x, chess_pos_y, chess_pos_z,
    chess_rot_x, chess_rot_y, chess_rot_z,
    square_size_dx, square_size_dy;

    // Screenshot Config
    public GameObject CameraConfig, ChessConfig, SaveConfig, BtnScreenshot, BtnBack;

    //Check Self active
    public GameObject calibActive;

    private int a, b, c;

    void Start()
    {
        a = 0;
        b = 0;
        c = 0;
        for (int i = 0; i < FishHigh.Length; i++)
        {
            FishHigh[i].SetActive(false);
        }
        for (int i = 0; i < FishLow.Length; i++)
        {
            FishLow[i].SetActive(true);
        }
        for (int i = 0; i < FishCapture.Length; i++)
        {
            FishCapture[i].SetActive(true);
        }
    }

    void Update()
    {
        if (calibActive.activeSelf == true && c == 0)
        {
            a = 0;
            b = 0;
            c = 1;
        }
        if (calibActive.activeSelf == false)
        {
            c = 0;
        }

        if (b < 10 && a < 1)
        {
            for (int i = 0; i < FishHigh.Length; i++)
            {
                FishHigh[i].SetActive(true);
            }
            for (int i = 0; i < FishLow.Length; i++)
            {
                FishLow[i].SetActive(false);
            }
            for (int i = 0; i < FishCapture.Length; i++)
            {
                FishCapture[i].SetActive(true);
            }
            b++;
        }
        else
        {
            a = 0;
            for (int i = 0; i < FishCapture.Length; i++)
            {
                FishCapture[i].SetActive(false);
            }
        }

        if (DroneConfig_1.transform.hasChanged)
        {
            for (int i = 0; i < FishHigh.Length; i++)
            {
                FishHigh[i].SetActive(false);
            }
            for (int i = 0; i < FishLow.Length; i++)
            {
                FishLow[i].SetActive(true);
            }
            
            cam_pos_x.text = (DroneConfig_1.transform.localPosition.y * 1000).ToString();
            cam_pos_y.text = (DroneConfig_1.transform.localPosition.x * 1000).ToString();
            cam_pos_z.text = (-DroneConfig_1.transform.localPosition.z * 1000).ToString();

            cam_rot_x.text = DroneConfig_1.transform.localRotation.eulerAngles.y.ToString();
            cam_rot_y.text = DroneConfig_1.transform.localRotation.eulerAngles.x.ToString();
            cam_rot_z.text = DroneConfig_1.transform.localRotation.eulerAngles.z.ToString();
            b = 0;
            print("The transform has changed!");
            DroneConfig_1.transform.hasChanged = false;
        }

        if (Chess.transform.hasChanged)
        {
            for (int i = 0; i < FishHigh.Length; i++)
            {
                FishHigh[i].SetActive(false);
            }
            for (int i = 0; i < FishLow.Length; i++)
            {
                FishLow[i].SetActive(true);
            }

            b = 0;
            print("The transform has changed!");
            Chess.transform.hasChanged = false;
        }

    }
    // Camera FOV
    public void chooseFOV_1()
    {
        if (camFOV_1.value == 0)
        {
            FOV_1[0].SetActive(true);
            FOV_1[1].SetActive(false);
            FOV_1[2].SetActive(false);
        }
        else if (camFOV_1.value == 1)
        {
            FOV_1[0].SetActive(false);
            FOV_1[1].SetActive(true);
            FOV_1[2].SetActive(false);
        }
        else if (camFOV_1.value == 2)
        {
            FOV_1[0].SetActive(false);
            FOV_1[1].SetActive(false);
            FOV_1[2].SetActive(true);
        }
    }

    // Camera Quality
    public void chooseCamQuality_1()
    {
        if (camQuality_1.value == 0) // Fast
        {
            for (int i = 0; i < CamFast_1.Length; i++)
            {
                CamFast_1[i].SetActive(true);
            }
            for (int i = 0; i < CamSimple_1.Length; i++)
            {
                CamSimple_1[i].SetActive(false);
            }
            for (int i = 0; i < CamGood_1.Length; i++)
            {
                CamGood_1[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty_1.Length; i++)
            {
                CamBeauty_1[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast_1.Length; i++)
            {
                CamFantast_1[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality_1.value == 1) // Simple
        {
            for (int i = 0; i < CamFast_1.Length; i++)
            {
                CamFast_1[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple_1.Length; i++)
            {
                CamSimple_1[i].SetActive(true);
            }
            for (int i = 0; i < CamGood_1.Length; i++)
            {
                CamGood_1[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty_1.Length; i++)
            {
                CamBeauty_1[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast_1.Length; i++)
            {
                CamFantast_1[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality_1.value == 2) // Good
        {
            for (int i = 0; i < CamFast_1.Length; i++)
            {
                CamFast_1[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple_1.Length; i++)
            {
                CamSimple_1[i].SetActive(false);
            }
            for (int i = 0; i < CamGood_1.Length; i++)
            {
                CamGood_1[i].SetActive(true);
            }
            for (int i = 0; i < CamBeauty_1.Length; i++)
            {
                CamBeauty_1[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast_1.Length; i++)
            {
                CamFantast_1[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality_1.value == 3) // Beautiful
        {
            for (int i = 0; i < CamFast_1.Length; i++)
            {
                CamFast_1[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple_1.Length; i++)
            {
                CamSimple_1[i].SetActive(false);
            }
            for (int i = 0; i < CamGood_1.Length; i++)
            {
                CamGood_1[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty_1.Length; i++)
            {
                CamBeauty_1[i].SetActive(true);
            }
            for (int i = 0; i < CamFantast_1.Length; i++)
            {
                CamFantast_1[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality_1.value == 4) // Fantastic
        {
            for (int i = 0; i < CamFast_1.Length; i++)
            {
                CamFast_1[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple_1.Length; i++)
            {
                CamSimple_1[i].SetActive(false);
            }
            for (int i = 0; i < CamGood_1.Length; i++)
            {
                CamGood_1[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty_1.Length; i++)
            {
                CamBeauty_1[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast_1.Length; i++)
            {
                CamFantast_1[i].SetActive(true);
            }
            b = 0;
            a = 0;
        }
    }

    // Camera Indication
    public void Camera_X_1()
    {
        cam_pos_x_manual.image.color = Color.yellow;
        cam_rot_x_manual.image.color = Color.yellow;
        cam_pos_x.image.color = Color.yellow;
        cam_rot_x.image.color = Color.yellow;
    }
    public void Camera_Y_1()
    {
        cam_pos_y_manual.image.color = Color.yellow;
        cam_rot_y_manual.image.color = Color.yellow;
        cam_pos_y.image.color = Color.yellow;
        cam_rot_y.image.color = Color.yellow;
    }
    public void Camera_Z_1()
    {
        cam_pos_z_manual.image.color = Color.yellow;
        cam_rot_z_manual.image.color = Color.yellow;
        cam_pos_z.image.color = Color.yellow;
        cam_rot_z.image.color = Color.yellow;
    }
    public void Camera_Reset_color_1()
    {
        cam_pos_x_manual.image.color = Color.white;
        cam_pos_y_manual.image.color = Color.white;
        cam_pos_z_manual.image.color = Color.white;
        cam_rot_x_manual.image.color = Color.white;
        cam_rot_y_manual.image.color = Color.white;
        cam_rot_z_manual.image.color = Color.white;
        cam_pos_x.image.color = Color.white;
        cam_pos_y.image.color = Color.white;
        cam_pos_z.image.color = Color.white;
        cam_rot_x.image.color = Color.white;
        cam_rot_y.image.color = Color.white;
        cam_rot_z.image.color = Color.white;
    }

    // Manual Toogle
    public void Cam_manual_Toogle(bool newValue)
    {
        for (int i = 0; i < Cam_control.Length; i++)
        {
            Cam_control[i].SetActive(newValue);
        }
        Cam_manual_text.SetActive(!newValue);

        cam_pos_x_manual.text = cam_pos_x.text;
        cam_pos_y_manual.text = cam_pos_y.text;
        cam_pos_z_manual.text = cam_pos_z.text;
        cam_rot_x_manual.text = cam_rot_x.text;
        cam_rot_y_manual.text = cam_rot_y.text;
        cam_rot_z_manual.text = cam_rot_z.text;

        GameObject.Find("DroneConfig").GetComponent<CameraOperate>().enabled = newValue;

        b = 0;
        a = 0;
    }

    // Camera Position
    public void CameraXYZ()
    {
        DroneConfig_1.transform.localPosition = new Vector3((float.Parse(cam_pos_y_manual.text)) / 1000, (float.Parse(cam_pos_x_manual.text)) / 1000, (-float.Parse(cam_pos_z_manual.text)) / 1000);
        b = 0;
        a = 0;
    }

    public void Reset_DroneXYZ_1()
    {
        DroneConfig_1.transform.localPosition = new Vector3(0, 0, 0);
        cam_pos_x.text = "0";
        cam_pos_y.text = "0";
        cam_pos_z.text = "0";
        cam_pos_x_manual.text = "0";
        cam_pos_y_manual.text = "0";
        cam_pos_z_manual.text = "0";
    }

    // Camera Orientation
    public void CameraXYZ_rot()
    {
        DroneConfig_1.transform.localRotation = Quaternion.Euler(float.Parse(cam_rot_y_manual.text), float.Parse(cam_rot_x_manual.text), float.Parse(cam_rot_z_manual.text));
        b = 0;
        a = 0;
    }

    public void Reset_DroneXYZ_rot_1()
    {
        DroneConfig_1.transform.localRotation = Quaternion.Euler(0, 0, 0);
        cam_rot_x.text = "0";
        cam_rot_y.text = "0";
        cam_rot_z.text = "0";
        cam_rot_x_manual.text = "0";
        cam_rot_y_manual.text = "0";
        cam_rot_z_manual.text = "0";
    }

    // Chess Indication
    public void Chess_X()
    {
        chess_pos_x.image.color = Color.yellow;
        chess_rot_x.image.color = Color.yellow;
    }
    public void Chess_Y()
    {
        chess_pos_y.image.color = Color.yellow;
        chess_rot_y.image.color = Color.yellow;
    }
    public void Chess_Z()
    {
        chess_pos_z.image.color = Color.yellow;
        chess_rot_z.image.color = Color.yellow;
    }
    public void Chess_Reset_color()
    {
        chess_pos_x.image.color = Color.white;
        chess_rot_x.image.color = Color.white;
        chess_pos_y.image.color = Color.white;
        chess_rot_y.image.color = Color.white;
        chess_pos_z.image.color = Color.white;
        chess_rot_z.image.color = Color.white;
    }

    // Pattern position
    public void ChessXYZ()
    {
        Chess.transform.localPosition = new Vector3((float.Parse(chess_pos_y.text))/1000, (float.Parse(chess_pos_z.text))/1000, (float.Parse(chess_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_ChessXYZ()
    {
        chess_pos_x.text = "0";
        chess_pos_y.text = "0";
        chess_pos_z.text = "-300";
        Chess.transform.localPosition = new Vector3((float.Parse(chess_pos_y.text)) / 1000, (float.Parse(chess_pos_z.text)) / 1000, (float.Parse(chess_pos_x.text)) / 1000);
        b = 0;
        a = 0;
    }
    // Pattern Rotation
    public void ChessXYZ_rot()
    {
        Chess.transform.localRotation = Quaternion.Euler(float.Parse(chess_rot_y.text), float.Parse(chess_rot_z.text), float.Parse(chess_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_ChessXYZ_rot()
    {
        chess_rot_x.text = "0";
        chess_rot_y.text = "0";
        chess_rot_z.text = "0";
        Chess.transform.localRotation = Quaternion.Euler(float.Parse(chess_rot_y.text), float.Parse(chess_rot_z.text), float.Parse(chess_rot_x.text));
        b = 0;
        a = 0;
    }

    // Scale Chess
    public void ChessPatternSize_1()
    {
        Chess.transform.localScale = new Vector3((float.Parse(square_size_dx.text))*10/969, 1, (float.Parse(square_size_dy.text))*10/969);
        b = 0;
        a = 0;
    }
    public void Reset_ChessPatternSize_1()
    {
        square_size_dx.text = "96.9";
        square_size_dy.text = "96.9";
        b = 0;
        a = 0;
    }

    public void TakeScreenshot()
    {
        CameraConfig.SetActive(false);
        ChessConfig.SetActive(false); 
        BtnScreenshot.SetActive(false);

        SaveConfig.SetActive(true);
        BtnBack.SetActive(true);
        b = 0;
        a = 0;
        c = 1;
    }
    public void BackScreenshot()
    {
        CameraConfig.SetActive(true);
        ChessConfig.SetActive(true);
        BtnScreenshot.SetActive(true);

        SaveConfig.SetActive(false);
        BtnBack.SetActive(false);

        for (int i = 0; i < FishHigh.Length; i++)
        {
            FishHigh[i].SetActive(false);
        }
        for (int i = 0; i < FishLow.Length; i++)
        {
            FishLow[i].SetActive(true);
        }
        for (int i = 0; i < FishCapture.Length; i++)
        {
            FishCapture[i].SetActive(true);
        }
        c = 0;
    }
}

