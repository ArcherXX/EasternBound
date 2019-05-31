using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetClassUIChanger : MonoBehaviour
{
    public GameObject unit;
    public UnitScript uscript;
    public Text targetClassUI;
    public int targetclass;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        unit = GameObject.FindGameObjectWithTag("Target");
        uscript = unit.GetComponent<UnitScript>();
        targetclass = uscript.ClassInt;
        if (targetclass == 0)
        {
            targetClassUI.text = "Class: Tank";
        }
        if (targetclass == 1)
        {
            targetClassUI.text = "Class: Healer";
        }
        if (targetclass == 2)
        {
            targetClassUI.text = "Class: Sword";
        }
        if (targetclass == 3)
        {
            targetClassUI.text = "Class: Sniper";
        }
    }
}