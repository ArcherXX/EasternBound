using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetArmorUIChanger : MonoBehaviour
{
    public GameObject unit;
    public UnitScript uscript;
    public Text targetArmorUI;
    public int targetarmor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        unit = GameObject.FindGameObjectWithTag("Target");
        uscript = unit.GetComponent<UnitScript>();
        targetarmor = uscript.armor;
        targetArmorUI.text = "Armor: " + targetarmor;
    }
}