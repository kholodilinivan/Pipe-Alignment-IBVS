using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurements_y : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public Camera ChooseCam;
    public GameObject[] Coord;

    void Start()
    {  
    }

    void Update()
    {       
    }

    private void OnMouseDown()
    {
        // mZCoord = ChooseCam.WorldToScreenPoint(gameObject.transform.position).z;
        mZCoord = ChooseCam.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();

        for (int i = 0; i < Coord.Length; i++)
        {
            Coord[i].transform.parent = gameObject.transform;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return ChooseCam.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
      //  transform.position = GetMouseWorldPos() + mOffset; // for all coordinates
        transform.position = new Vector3(gameObject.transform.position.x,GetMouseWorldPos().y + mOffset.y, gameObject.transform.position.z);
    }

    private void OnMouseUp()
    {
        for (int i = 0; i < Coord.Length; i++)
        {
            Coord[i].transform.parent = GameObject.Find("measurement").transform;
        }
    }
}
