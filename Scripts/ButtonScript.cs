using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    public int ID;
    // Use this for initialization
    private int counter;
    void Start() {
        counter = 5;
        
	}
	
	// Update is called once per frame
	void Update () {
        counter--;
        if(counter == 0)
        {
            int ClassyBoi = this.GetComponentInParent<UnitScript>().ClassInt;
            string MyName = this.GetComponentInParent<UnitScript>().Classes[ClassyBoi].attacks[ID].attackName;
            this.GetComponentInChildren<Text>().text = MyName;
        }
	}
}
