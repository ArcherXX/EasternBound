using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour
{
    public GameObject dropdownlabel;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Text spritechange = dropdownlabel.GetComponent<Text>();
        //Debug.Log(spritechange);

    }
}
