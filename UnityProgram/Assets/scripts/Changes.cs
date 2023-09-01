using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changes : MonoBehaviour
{
    public GameObject[] Fisheye, FishPic, FishPicMove;
    // public GameObject FishPic, FishPicMove;
    private int a, b, c;
    public int d;
    // Menu changes
    public GameObject walleActive;

    // for Canvas Settings

    // Camera Config
    public GameObject[] DroneConfig, FOV, CamFast, CamSimple, CamGood, CamBeauty, CamFantast;
    Vector3 val_drone;
    public Slider slider_x, slider_y, slider_z;
    public InputField rot_cam_x, rot_cam_y, rot_cam_z;
    public Dropdown camFOV, camQuality;

    // Laser Config
    public GameObject LasActive; 
    public InputField las_pos_x, las_pos_y, las_pos_z, las_rot_x, las_rot_y, las_rot_z,
    las_scale_x, las_scale_y, las_scale_z;
    public Material LaserMaterial;

    // Checkerboards Config
    public GameObject LeftChess, RightChess, BottomChess;
    public InputField leftchess_pos_x, leftchess_pos_y, leftchess_pos_z, rightchess_pos_x, rightchess_pos_y, rightchess_pos_z,
    leftchess_rot_x, leftchess_rot_y, leftchess_rot_z, rightchess_rot_x, rightchess_rot_y, rightchess_rot_z,
    bottomchess_pos_x, bottomchess_pos_y, bottomchess_pos_z, bottomchess_rot_x, bottomchess_rot_y, bottomchess_rot_z,
    square_dx, square_dy;

    // Cube Config
    public GameObject Target;
    public InputField lefttarget_pos, fronttarget_pos, righttarget_pos;

    // Obstacles Config
    public GameObject LeftCube, FrontCube, RightCube;
    public InputField leftcube_pos_x, leftcube_pos_y, leftcube_pos_z, leftcube_rot_x, leftcube_rot_y, leftcube_rot_z,
    frontcube_pos_x, frontcube_pos_y, frontcube_pos_z, frontcube_rot_x, frontcube_rot_y, frontcube_rot_z,
    rightcube_pos_x, rightcube_pos_y, rightcube_pos_z, rightcube_rot_x, rightcube_rot_y, rightcube_rot_z;

    // Robot Config
    public GameObject Robot, Robot_Hide, Text_manual;
    public InputField robot_pos_x, robot_pos_y, robot_rot, robot_speed_fow, robot_speed_rot,
        robot_pos_x_manual, robot_pos_y_manual, robot_rot_manual;

    // Other Config
    public GameObject [] Floor, Robot_keyboard;

    void Start()
    {
      //  rot_cam_x.text = "0";

        a = 0;
        b = 0;
        c = 0;
        d = 0;
        for (int i = 0; i < Fisheye.Length; i++)
        {
            Fisheye[i].SetActive(false);
        }
        for (int i = 0; i < FishPic.Length; i++)
        {
            FishPic[i].SetActive(false);
        }
        for (int i = 0; i < FishPicMove.Length; i++)
        {
            FishPicMove[i].SetActive(true);
        }
      //  FishPic.SetActive(false);
      //  FishPicMove.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            b = -10;
            a = 0;
        }

        if (walleActive.activeSelf == true && c == 0)
        {
            a = 0;
            b = 0;
            c = 1; 
        }
        if (walleActive.activeSelf == false)
        {
            c = 0;
        }

        // control any object transformations (need to attatch to every object)
        if (transform.hasChanged)
        {
            print("The transform has changed!");
            transform.hasChanged = false;
            //   FishPic.SetActive(false);
            //   FishPicMove.SetActive(false);
            if (d == 0)
            {
                for (int i = 0; i < FishPicMove.Length; i++)
                {
                    FishPicMove[i].SetActive(false);
                }
                for (int i = 0; i < Fisheye.Length; i++)
                {
                    Fisheye[i].SetActive(true);
                }

                a++;
                b = 0;
            }
            // Robot Pos and Rot
            robot_pos_x.text = ((Robot.transform.localPosition.z) * 1000).ToString();
            robot_pos_y.text = ((Robot.transform.localPosition.x) * 1000).ToString();
            robot_rot.text = (Robot.transform.localRotation.eulerAngles.y).ToString();
            d = 0;
        }
        else
        {
            for (int i = 0; i < Fisheye.Length; i++)
            {
                Fisheye[i].SetActive(false);
            }
            a = 0;
        }

        if (b < 10 && a < 1)
        {
            for (int i = 0; i < FishPic.Length; i++)
            {
                FishPic[i].SetActive(true);
            }
            for (int i = 0; i < FishPicMove.Length; i++)
            {
                FishPicMove[i].SetActive(true);
            }

         //   FishPic.SetActive(true);
         //   FishPicMove.SetActive(true);
            b++;
        }
        else
        {
            for (int i = 0; i < FishPic.Length; i++)
            {
                FishPic[i].SetActive(false);
            }
         //   FishPic.SetActive(false);

            // FishPicMove.SetActive(true);
        }
    }

    public void Reset_syst()
    {
        a = 0;
        b = 0;
    }

    // for Canvas Settings

//Camera Config
    // Camera Indication
    public void Camera_X()
    {
        rot_cam_x.image.color = Color.yellow;
    }
    public void Camera_Y()
    {
        rot_cam_y.image.color = Color.yellow;
    }
    public void Camera_Z()
    {
        rot_cam_z.image.color = Color.yellow;
    }
    public void Camera_Reset_color()
    {
        rot_cam_x.image.color = Color.white;
        rot_cam_y.image.color = Color.white;
        rot_cam_z.image.color = Color.white;
    }

    // Camera Position
    public void DroneXYZ()
    {
        //  Debug.Log("New Val" + slider_x.value);
        for (int i = 0; i < DroneConfig.Length; i++)
        {
            DroneConfig[i].transform.localPosition = new Vector3(slider_y.value, slider_z.value, slider_x.value);
        }
        b = 0;
        a = 0;
    }
    public void Reset_DroneXYZ()
    {
        slider_x.value = 0;
        slider_y.value = 0;
        slider_z.value = 0;
        b = 0;
        a = 0;
    }

    // Camera Orientation
    public void DroneXYZ_rot()
    {
        for (int i = 0; i < DroneConfig.Length; i++)
        {
            DroneConfig[i].transform.localRotation = Quaternion.Euler(float.Parse(rot_cam_y.text), float.Parse(rot_cam_x.text), float.Parse(rot_cam_z.text));
        }
        b = 0;
        a = 0;
    }

    public void Reset_DroneXYZ_rot()
    {
        rot_cam_x.text = "0";
        rot_cam_y.text = "0";
        rot_cam_z.text = "0";
        b = 0;
        a = 0;
    }

    // Camera FOV
    public void chooseFOV()
    {
        if (camFOV.value == 0)
        {
            FOV[0].SetActive(true);
            FOV[1].SetActive(false);
            FOV[2].SetActive(false);
        }
        else if (camFOV.value == 1)
        {
            FOV[0].SetActive(false);
            FOV[1].SetActive(true);
            FOV[2].SetActive(false);
        }
        else if (camFOV.value == 2)
        {
            FOV[0].SetActive(false);
            FOV[1].SetActive(false);
            FOV[2].SetActive(true);
        }
    }

    // Camera Quality
    public void chooseCamQuality()
    {
        if (camQuality.value == 0) // Fast
        {
            for (int i = 0; i < CamFast.Length; i++)
            {
                CamFast[i].SetActive(true);
            }
            for (int i = 0; i < CamSimple.Length; i++)
            {
                CamSimple[i].SetActive(false);
            }
            for (int i = 0; i < CamGood.Length; i++)
            {
                CamGood[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty.Length; i++)
            {
                CamBeauty[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast.Length; i++)
            {
                CamFantast[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality.value == 1) // Simple
        {
            for (int i = 0; i < CamFast.Length; i++)
            {
                CamFast[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple.Length; i++)
            {
                CamSimple[i].SetActive(true);
            }
            for (int i = 0; i < CamGood.Length; i++)
            {
                CamGood[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty.Length; i++)
            {
                CamBeauty[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast.Length; i++)
            {
                CamFantast[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality.value == 2) // Good
        {
            for (int i = 0; i < CamFast.Length; i++)
            {
                CamFast[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple.Length; i++)
            {
                CamSimple[i].SetActive(false);
            }
            for (int i = 0; i < CamGood.Length; i++)
            {
                CamGood[i].SetActive(true);
            }
            for (int i = 0; i < CamBeauty.Length; i++)
            {
                CamBeauty[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast.Length; i++)
            {
                CamFantast[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality.value == 3) // Beautiful
        {
            for (int i = 0; i < CamFast.Length; i++)
            {
                CamFast[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple.Length; i++)
            {
                CamSimple[i].SetActive(false);
            }
            for (int i = 0; i < CamGood.Length; i++)
            {
                CamGood[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty.Length; i++)
            {
                CamBeauty[i].SetActive(true);
            }
            for (int i = 0; i < CamFantast.Length; i++)
            {
                CamFantast[i].SetActive(false);
            }
            b = 0;
            a = 0;
        }
        else if (camQuality.value == 4) // Fantastic
        {
            for (int i = 0; i < CamFast.Length; i++)
            {
                CamFast[i].SetActive(false);
            }
            for (int i = 0; i < CamSimple.Length; i++)
            {
                CamSimple[i].SetActive(false);
            }
            for (int i = 0; i < CamGood.Length; i++)
            {
                CamGood[i].SetActive(false);
            }
            for (int i = 0; i < CamBeauty.Length; i++)
            {
                CamBeauty[i].SetActive(false);
            }
            for (int i = 0; i < CamFantast.Length; i++)
            {
                CamFantast[i].SetActive(true);
            }
            b = 0;
            a = 0;
        }
    }

// Laser Config
public void LaserToogle(bool newValue)
    {
        LasActive.SetActive(newValue);
        b = 0;
        a = 0;
    }

    // Laser Indication
    public void Laser_X()
    {
        las_pos_x.image.color = Color.yellow;
        las_rot_x.image.color = Color.yellow;
    }
    public void Laser_Y()
    {
        las_pos_y.image.color = Color.yellow;
        las_rot_y.image.color = Color.yellow;
    }
    public void Laser_Z()
    {
        las_pos_z.image.color = Color.yellow;
        las_rot_z.image.color = Color.yellow;
    }
    public void Laser_Reset_color()
    {
        las_pos_x.image.color = Color.white;
        las_rot_x.image.color = Color.white;
        las_pos_y.image.color = Color.white;
        las_rot_y.image.color = Color.white;
        las_pos_z.image.color = Color.white;
        las_rot_z.image.color = Color.white;
    }

    //Laser Position
    public void LaserXYZ()
    {
        LasActive.transform.localPosition = new Vector3((float.Parse(las_pos_y.text))/1000, (float.Parse(las_pos_z.text))/1000, (float.Parse(las_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_LaserXYZ()
    {
        las_pos_x.text = "0";
        las_pos_y.text = "0";
        las_pos_z.text = "-666.023";
        b = 0;
        a = 0;
    }
    // Laser Rotation
    public void LaserXYZ_rot()
    {
        LasActive.transform.localRotation = Quaternion.Euler(float.Parse(las_rot_y.text), float.Parse(las_rot_z.text), float.Parse(las_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_LaserXYZ_rot()
    {
        las_rot_x.text = "0";
        las_rot_y.text = "0";
        las_rot_z.text = "0";
        b = 0;
        a = 0;
    }
    // Laser Scale
    public void LaserXYZ_scale()
    {
        LasActive.transform.localScale = new Vector3(float.Parse(las_scale_y.text), 1, float.Parse(las_scale_x.text));
        LaserMaterial.SetFloat("_IntersectionMax", 607*float.Parse(las_scale_z.text)/1000);
        b = 0;
        a = 0;
    }
    public void Reset_LaserXYZ_scale()
    {
        las_scale_x.text = "1";
        las_scale_y.text = "1";
        las_scale_z.text = "1";
        b = 0;
        a = 0;
    }

    // Target Config
    public void TargetToogle(bool newValue)
    {
        Target.SetActive(newValue);
        b = 0;
        a = 0;
    }
    // Target Indication
    public void Target_left()
    {
        lefttarget_pos.image.color = Color.yellow;
    }
    public void Target_front()
    {
        fronttarget_pos.image.color = Color.yellow;
    }
    public void Target_right()
    {
        righttarget_pos.image.color = Color.yellow;
    }
    public void Target_Reset_color()
    {
        lefttarget_pos.image.color = Color.white;
        fronttarget_pos.image.color = Color.white;
        righttarget_pos.image.color = Color.white;
    }

    // Checkerboards Config
    public void LeftChessToogle(bool newValue)
    {
        LeftChess.SetActive(newValue);
        b = 0;
        a = 0;
    }
    public void RightChessToogle(bool newValue)
    {
        RightChess.SetActive(newValue);
        b = 0;
        a = 0;
    }
    public void BottomChessToogle(bool newValue)
    {
        BottomChess.SetActive(newValue);
        b = 0;
        a = 0;
    }

    // Chess Left Indication
    public void Chess_left_X()
    {
        leftchess_pos_x.image.color = Color.yellow;
        leftchess_rot_x.image.color = Color.yellow;
    }
    public void Chess_left_Y()
    {
        leftchess_pos_y.image.color = Color.yellow;
        leftchess_rot_y.image.color = Color.yellow;
    }
    public void Chess_left_Z()
    {
        leftchess_pos_z.image.color = Color.yellow;
        leftchess_rot_z.image.color = Color.yellow;
    }
    public void Chess_left_Reset_color()
    {
        leftchess_pos_x.image.color = Color.white;
        leftchess_rot_x.image.color = Color.white;
        leftchess_pos_y.image.color = Color.white;
        leftchess_rot_y.image.color = Color.white;
        leftchess_pos_z.image.color = Color.white;
        leftchess_rot_z.image.color = Color.white;
    }

    // Chess Right Indication
    public void Chess_right_X()
    {
        rightchess_pos_x.image.color = Color.yellow;
        rightchess_rot_x.image.color = Color.yellow;
    }
    public void Chess_right_Y()
    {
        rightchess_pos_y.image.color = Color.yellow;
        rightchess_rot_y.image.color = Color.yellow;
    }
    public void Chess_right_Z()
    {
        rightchess_pos_z.image.color = Color.yellow;
        rightchess_rot_z.image.color = Color.yellow;
    }
    public void Chess_right_Reset_color()
    {
        rightchess_pos_x.image.color = Color.white;
        rightchess_rot_x.image.color = Color.white;
        rightchess_pos_y.image.color = Color.white;
        rightchess_rot_y.image.color = Color.white;
        rightchess_pos_z.image.color = Color.white;
        rightchess_rot_z.image.color = Color.white;
    }

    // Chess Bottom Indication
    public void Chess_bottom_X()
    {
        bottomchess_pos_x.image.color = Color.yellow;
        bottomchess_rot_x.image.color = Color.yellow;
    }
    public void Chess_bottom_Y()
    {
        bottomchess_pos_y.image.color = Color.yellow;
        bottomchess_rot_y.image.color = Color.yellow;
    }
    public void Chess_bottom_Z()
    {
        bottomchess_pos_z.image.color = Color.yellow;
        bottomchess_rot_z.image.color = Color.yellow;
    }
    public void Chess_bottom_Reset_color()
    {
        bottomchess_pos_x.image.color = Color.white;
        bottomchess_rot_x.image.color = Color.white;
        bottomchess_pos_y.image.color = Color.white;
        bottomchess_rot_y.image.color = Color.white;
        bottomchess_pos_z.image.color = Color.white;
        bottomchess_rot_z.image.color = Color.white;
    }

    // Left pattern position
    public void LeftChessXYZ()
    {
        LeftChess.transform.localPosition = new Vector3((float.Parse(leftchess_pos_y.text))/1000, (float.Parse(leftchess_pos_z.text))/1000, (float.Parse(leftchess_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_LeftChessXYZ()
    {
        leftchess_pos_x.text = "-464";
        leftchess_pos_y.text = "-646";
        leftchess_pos_z.text = "-232.9292";
        b = 0;
        a = 0;
    }
    // Left pattern Rotation
    public void LeftChessXYZ_rot()
    {
        LeftChess.transform.localRotation = Quaternion.Euler(float.Parse(leftchess_rot_y.text), float.Parse(leftchess_rot_z.text), float.Parse(leftchess_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_LeftChessXYZ_rot()
    {
        leftchess_rot_x.text = "0";
        leftchess_rot_y.text = "0";
        leftchess_rot_z.text = "0";
        b = 0;
        a = 0;
    }

    // Rigth pattern position
    public void RightChessXYZ()
    {
        RightChess.transform.localPosition = new Vector3((float.Parse(rightchess_pos_y.text))/1000, (float.Parse(rightchess_pos_z.text))/1000, (float.Parse(rightchess_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_RightChessXYZ()
    {
        rightchess_pos_x.text = "-573";
        rightchess_pos_y.text = "333";
        rightchess_pos_z.text = "-232.9292";
        b = 0;
        a = 0;
    }
    // Right pattern Rotation
    public void RightChessXYZ_rot()
    {
        RightChess.transform.localRotation = Quaternion.Euler(float.Parse(rightchess_rot_y.text), float.Parse(rightchess_rot_z.text), float.Parse(rightchess_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_RightChessXYZ_rot()
    {
        rightchess_rot_x.text = "0";
        rightchess_rot_y.text = "0";
        rightchess_rot_z.text = "180";
        b = 0;
        a = 0;
    }

    // Bottom pattern position
    public void BottomChessXYZ()
    {
        BottomChess.transform.localPosition = new Vector3((float.Parse(bottomchess_pos_y.text)) / 1000, (float.Parse(bottomchess_pos_z.text)) / 1000, (float.Parse(bottomchess_pos_x.text)) / 1000);
        b = 0;
        a = 0;
    }
    public void Reset_BottomChessXYZ()
    {
        bottomchess_pos_x.text = "235";
        bottomchess_pos_y.text = "-360";
        bottomchess_pos_z.text = "-666.023";
        b = 0;
        a = 0;
    }
    // Right pattern Rotation
    public void BottomChessXYZ_rot()
    {
        BottomChess.transform.localRotation = Quaternion.Euler(float.Parse(bottomchess_rot_y.text), float.Parse(bottomchess_rot_z.text), float.Parse(bottomchess_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_BottomChessXYZ_rot()
    {
        bottomchess_rot_x.text = "0";
        bottomchess_rot_y.text = "0";
        bottomchess_rot_z.text = "0";
        b = 0;
        a = 0;
    }

    // Scale Chess
    public void ChessPatternSize()
    {
        RightChess.transform.localScale = new Vector3((float.Parse(square_dx.text))*10/969, (float.Parse(square_dy.text))*10/969, 1);
        LeftChess.transform.localScale = new Vector3(1, (float.Parse(square_dy.text))*10/969, (float.Parse(square_dx.text))*10/969);
        BottomChess.transform.localScale = new Vector3((float.Parse(square_dx.text)) * 10 / 969, 1, (float.Parse(square_dy.text))*10/969);
        b = 0;
        a = 0;
    }
    public void Reset_ChessPatternSize()
    {
        square_dx.text = "96.9";
        square_dy.text = "96.9";
        b = 0;
        a = 0;
    }

// Obstacles Config
    public void LeftCubeToogle(bool newValue)
    {
        LeftCube.SetActive(newValue);
        b = 0;
        a = 0;
    }
    public void FrontCubeToogle(bool newValue)
    {
        FrontCube.SetActive(newValue);
        b = 0;
        a = 0;
    }
    public void RightCubeToogle(bool newValue)
    {
        RightCube.SetActive(newValue);
        b = 0;
        a = 0;
    }

    // Cube Left Indication
    public void Cube_left_X()
    {
        leftcube_pos_x.image.color = Color.yellow;
        leftcube_rot_x.image.color = Color.yellow;
    }
    public void Cube_left_Y()
    {
        leftcube_pos_y.image.color = Color.yellow;
        leftcube_rot_y.image.color = Color.yellow;
    }
    public void Cube_left_Z()
    {
        leftcube_pos_z.image.color = Color.yellow;
        leftcube_rot_z.image.color = Color.yellow;
    }
    public void Cube_left_Reset_color()
    {
        leftcube_pos_x.image.color = Color.white;
        leftcube_rot_x.image.color = Color.white;
        leftcube_pos_y.image.color = Color.white;
        leftcube_rot_y.image.color = Color.white;
        leftcube_pos_z.image.color = Color.white;
        leftcube_rot_z.image.color = Color.white;
    }

    // Cube Front Indication
    public void Cube_front_X()
    {
        frontcube_pos_x.image.color = Color.yellow;
        frontcube_rot_x.image.color = Color.yellow;
    }
    public void Cube_front_Y()
    {
        frontcube_pos_y.image.color = Color.yellow;
        frontcube_rot_y.image.color = Color.yellow;
    }
    public void Cube_front_Z()
    {
        frontcube_pos_z.image.color = Color.yellow;
        frontcube_rot_z.image.color = Color.yellow;
    }
    public void Cube_front_Reset_color()
    {
        frontcube_pos_x.image.color = Color.white;
        frontcube_rot_x.image.color = Color.white;
        frontcube_pos_y.image.color = Color.white;
        frontcube_rot_y.image.color = Color.white;
        frontcube_pos_z.image.color = Color.white;
        frontcube_rot_z.image.color = Color.white;
    }

    // Cube Right Indication
    public void Cube_right_X()
    {
        rightcube_pos_x.image.color = Color.yellow;
        rightcube_rot_x.image.color = Color.yellow;
    }
    public void Cube_right_Y()
    {
        rightcube_pos_y.image.color = Color.yellow;
        rightcube_rot_y.image.color = Color.yellow;
    }
    public void Cube_right_Z()
    {
        rightcube_pos_z.image.color = Color.yellow;
        rightcube_rot_z.image.color = Color.yellow;
    }
    public void Cube_right_Reset_color()
    {
        rightcube_pos_x.image.color = Color.white;
        rightcube_rot_x.image.color = Color.white;
        rightcube_pos_y.image.color = Color.white;
        rightcube_rot_y.image.color = Color.white;
        rightcube_pos_z.image.color = Color.white;
        rightcube_rot_z.image.color = Color.white;
    }

    // Left cube position
    public void LeftCubeXYZ()
    {
        LeftCube.transform.localPosition = new Vector3((float.Parse(leftcube_pos_y.text))/1000, (float.Parse(leftcube_pos_z.text))/1000, (float.Parse(leftcube_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_LeftCubeXYZ()
    {
        leftcube_pos_x.text = "-375.5";
        leftcube_pos_y.text = "-466";
        leftcube_pos_z.text = "-386.6";
        b = 0;
        a = 0;
    }
    // Left cube Rotation
    public void LeftCubeXYZ_rot()
    {
        LeftCube.transform.localRotation = Quaternion.Euler(float.Parse(leftcube_rot_y.text), float.Parse(leftcube_rot_z.text), float.Parse(leftcube_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_LeftCubeXYZ_rot()
    {
        leftcube_rot_x.text = "0";
        leftcube_rot_y.text = "0";
        leftcube_rot_z.text = "0";
        b = 0;
        a = 0;
    }

    // Front cube position
    public void FrontCubeXYZ()
    {
        FrontCube.transform.localPosition = new Vector3((float.Parse(frontcube_pos_y.text))/1000, (float.Parse(frontcube_pos_z.text))/1000, (float.Parse(frontcube_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_FrontCubeXYZ()
    {
        frontcube_pos_x.text = "992";
        frontcube_pos_y.text = "-400";
        frontcube_pos_z.text = "-386.6";
        b = 0;
        a = 0;
    }
    // Front cube Rotation
    public void FrontCubeXYZ_rot()
    {
        FrontCube.transform.localRotation = Quaternion.Euler(float.Parse(frontcube_rot_y.text), float.Parse(frontcube_rot_z.text), float.Parse(frontcube_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_FrontCubeXYZ_rot()
    {
        frontcube_rot_x.text = "0";
        frontcube_rot_y.text = "0";
        frontcube_rot_z.text = "0";
        b = 0;
        a = 0;
    }

    // Right cube position
    public void RightCubeXYZ()
    {
        RightCube.transform.localPosition = new Vector3((float.Parse(rightcube_pos_y.text))/1000, (float.Parse(rightcube_pos_z.text))/1000, (float.Parse(rightcube_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_RightCubeXYZ()
    {
        rightcube_pos_x.text = "457.82";
        rightcube_pos_y.text = "1584";
        rightcube_pos_z.text = "-386.6";
        b = 0;
        a = 0;
    }
    // Right cube Rotation
    public void RightCubeXYZ_rot()
    {
        RightCube.transform.localRotation = Quaternion.Euler(float.Parse(rightcube_rot_y.text), float.Parse(rightcube_rot_z.text), float.Parse(rightcube_rot_x.text));
        b = 0;
        a = 0;
    }
    public void Reset_RightCubeXYZ_rot()
    {
        rightcube_rot_x.text = "0";
        rightcube_rot_y.text = "0";
        rightcube_rot_z.text = "0";
        b = 0;
        a = 0;
    }

// Robot Config
    public void Robot_HideToogle(bool newValue)
    {
        Robot_Hide.SetActive(newValue);
        b = 0;
        a = 0;
    }
    // Manual Toogle
    public void Manual_Toogle(bool newValue)
    {
        for (int i = 0; i < Robot_keyboard.Length; i++)
        {
            Robot_keyboard[i].SetActive(newValue);
        }
        Text_manual.SetActive(!newValue);

        robot_pos_x_manual.text = robot_pos_x.text;
        robot_pos_y_manual.text = robot_pos_y.text;
        robot_rot_manual.text = robot_rot.text;

        GameObject.Find("walle").GetComponent<walle>().enabled = newValue;

        b = 0;
        a = 0;
    }

    public void RobotToogle(bool newValue)
    {
        Robot.SetActive(newValue);
        b = 0;
        a = 0;
    }

    // Robot position
    public void RobotXYZ_Manual()
    {
        Robot.transform.localPosition = new Vector3((float.Parse(robot_pos_y_manual.text))/1000, 0, (float.Parse(robot_pos_x_manual.text))/1000);
        b = 0;
        a = 0;
    }
    public void RobotXYZ()
    {
      //  Robot.transform.localPosition = new Vector3((float.Parse(robot_pos_y.text))/1000, 0, (float.Parse(robot_pos_x.text))/1000);
        b = 0;
        a = 0;
    }
    public void Reset_RobotXYZ()
    {
        robot_pos_x.text = "0";
        robot_pos_y.text = "0";
        robot_pos_x_manual.text = "0";
        robot_pos_y_manual.text = "0";
        Robot.transform.localPosition = new Vector3(0, 0, 0);
        b = 0;
        a = 0;
    }

    // Robot Rotation
    public void RobotXYZ_rot_Manual()
    {
        Robot.transform.localRotation = Quaternion.Euler(0, float.Parse(robot_rot_manual.text), 0);
        b = 0;
        a = 0;
    }
    public void RobotXYZ_rot()
    {
     //   Robot.transform.localRotation = Quaternion.Euler(0, float.Parse(robot_rot.text), 0);
        b = 0;
        a = 0;
    }
    public void Reset_RobotXYZ_rot()
    {
        robot_rot.text = "0";
        robot_rot_manual.text = "0";
        Robot.transform.localRotation = Quaternion.Euler(0, 0, 0);
        b = 0;
        a = 0;
    }

    public void Reset_Robot_all()
    {
        robot_pos_x.text = "0";
        robot_pos_y.text = "0";
        robot_rot.text = "0";
        robot_pos_x_manual.text = "0";
        robot_pos_y_manual.text = "0";
        robot_rot_manual.text = "0";
        Robot.transform.localPosition = new Vector3(0, 0, 0);
        Robot.transform.localRotation = Quaternion.Euler(0, 0, 0);
        b = 0;
        a = 0;
    }

    public void StopMotion(bool newValue) // for motion
    {
        GameObject.Find("walle").GetComponent<walle>().enabled = newValue;
    }

    public void EffectButtons()
    {
        b = 0;
        a = 0;
    }

    // Other Config
    public void FloorToogle(bool newValue)
    {
        for (int i = 0; i < Floor.Length; i++)
        {
            Floor[i].SetActive(newValue);
        }
        b = 0;
        a = 0;
    }
}
