using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedArmorUIChanger : MonoBehaviour
{
    public GameObject unit;
    public UnitScript uscript;
    public Text selectedArmorUI;
    public int selectarmor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unit = GameObject.FindGameObjectWithTag("Selected");
        uscript = unit.GetComponent<UnitScript>();
        selectarmor = uscript.armor;
        selectedArmorUI.text = "Armor: " + selectarmor;
    }
}
