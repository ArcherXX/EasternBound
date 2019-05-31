using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    // Use this for initialization
    public Vector2 Mouse;
    void Start()
    {
        MouseUp = false;
    }
    public bool MouseUp;
    // Update is called once per frame

    private void OnMouseDown()
    {
        
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        //Debug.Log("Mouse is at :" + Input.mousePosition.x + ", " + Input.mousePosition.y);
        if (Input.mousePosition.x <= 450 && Input.mousePosition.x >= 70 && Input.mousePosition.y <= 100 && Input.mousePosition.y >= 45) { }
        else if (Input.GetMouseButtonDown(0) == true && MouseUp)
        {
            
            MouseUp = false;
            if (GameObject.FindGameObjectWithTag("Target"))
            {
                GameObject.FindGameObjectWithTag("Target").GetComponent<UnitScript>().retag();
            }
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.GetComponentInParent<UnitScript>().selected != true)
                {
                    hitInfo.collider.GetComponentInParent<UnitScript>().target();
                }
            }

        }
        if (Input.GetMouseButtonDown(0)==false &&MouseUp == false)
        {
            MouseUp = true;
        }

    }
}