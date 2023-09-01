using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class walle_pipe : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float rotSpeed;
    public int d;

    public InputField SetSpeed, SetRotSpeed;

    void Start()
    {
        player = (GameObject)this.gameObject;
        d = 0;
    }

    void Update()
    {
        if (d == 0)
        {
            speed = int.Parse(SetSpeed.text);
            rotSpeed = int.Parse(SetRotSpeed.text);

            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    player.transform.position += player.transform.up * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    player.transform.position -= player.transform.up * speed * Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    player.transform.position += player.transform.right * speed * Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    player.transform.position -= player.transform.right * speed * Time.deltaTime;
                }
            }
        }
        d = 0;
    }

    public void Reset_Robot_speed()
    {
        SetSpeed.text = "2";
        SetRotSpeed.text = "3";
    }
}