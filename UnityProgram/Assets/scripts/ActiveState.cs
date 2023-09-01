using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : MonoBehaviour
{
    public GameObject[] Fisheye, ActiveControl, FishPic, FishPicMove;
    private int a, b, c0, c1, c2, c3, c4, c5;

    void Start()
    {
        a = 0;
        b = 0;
        c0 = 0;
        c1 = 0;
        c2 = 0;
        c3 = 0;
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
    }

    void Update()
    {
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

        // control Active state condition
        if (ActiveControl[0].activeSelf == true && c0 == 0)
        {
            b = 0;
            a = 0;
            c0 = 1;
        }
        if (ActiveControl[0].activeSelf == false && c0 == 1)
        {
            b = 0;
            a = 0;
            c0 = 0;
        }

        // second
        if (ActiveControl[1].activeSelf == true && c1 == 0)
        {
            b = 0;
            a = 0;
            c1 = 1;
        }
        if (ActiveControl[1].activeSelf == false && c1 == 1)
        {
            b = 0;
            a = 0;
            c1 = 0;
        }

        // third
        if (ActiveControl[2].activeSelf == true && c2 == 0)
        {
            b = 0;
            a = 0;
            c2 = 1;
        }
        if (ActiveControl[2].activeSelf == false && c2 == 1)
        {
            b = 0;
            a = 0;
            c2 = 0;
        }

        // forth
        if (ActiveControl[3].activeSelf == true && c3 == 0)
        {
            b = 0;
            a = 0;
            c3 = 1;
        }
        if (ActiveControl[3].activeSelf == false && c3 == 1)
        {
            b = 0;
            a = 0;
            c3 = 0;
        }

        // fifth
        if (ActiveControl[4].activeSelf == true && c4 == 0)
        {
            b = 0;
            a = 0;
            c4 = 1;
        }
        if (ActiveControl[4].activeSelf == false && c4 == 1)
        {
            b = 0;
            a = 0;
            c4 = 0;
        }

        // sixth
        if (ActiveControl[5].activeSelf == true && c5 == 0)
        {
            b = 0;
            a = 0;
            c5 = 1;
        }
        if (ActiveControl[5].activeSelf == false && c5 == 1)
        {
            b = 0;
            a = 0;
            c5 = 0;
        }

    }


}
