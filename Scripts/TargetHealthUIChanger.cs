using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealthUIChanger : MonoBehaviour
{
    public GameObject unit;
    public UnitScript uscript;
    public Text targetHealthUI;
    public int targethealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unit = GameObject.FindGameObjectWithTag("Target");
        uscript = unit.GetComponent<UnitScript>();
        targethealth = uscript.health;
        targetHealthUI.text = "Health: " + targethealth;
    }
}
