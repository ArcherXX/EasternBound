using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedClassUIChanger : MonoBehaviour
{
    public GameObject unit;
    public UnitScript uscript;
    public Text selectedClassUI;
    public int selectclass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unit = GameObject.FindGameObjectWithTag("Selected");
        uscript = unit.GetComponent<UnitScript>();
        selectclass = uscript.ClassInt;
        if (selectclass == 0)
        {
            selectedClassUI.text = "Class: Tank";
        }
        if (selectclass == 1)
        {
            selectedClassUI.text = "Class: Healer";
        }
        if (selectclass == 2)
        {
            selectedClassUI.text = "Class: Sword";
        }
        if (selectclass == 3)
        {
            selectedClassUI.text = "Class: Sniper";
        }
    }
}
