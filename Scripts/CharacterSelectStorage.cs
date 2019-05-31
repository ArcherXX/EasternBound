using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectStorage : MonoBehaviour
{
    public Text character1;
    public Text character2;
    public Text character3;
    public Text character4;

    public int[] StorageList;

    // Start is called before the first frame update
    void Start()
    {
        StorageList = new int[4];
    }

    // Update is called once per frame
    void Update()
    {
        //Character 1

        if(character1.text == "Tank")
        {
            StorageList[0] = 1;
        }
        if (character1.text == "Healer")
        {
            StorageList[0] = 2;
        }
        if (character1.text == "Sword")
        {
            StorageList[0] = 3;
        }
        if (character1.text == "Sniper")
        {
            StorageList[0] = 4;
        }

        //Character 2

        if (character2.text == "Tank")
        {
            StorageList[1] = 1;
        }
        if (character2.text == "Healer")
        {
            StorageList[1] = 2;
        }
        if (character2.text == "Sword")
        {
            StorageList[1] = 3;
        }
        if (character2.text == "Sniper")
        {
            StorageList[1] = 4;
        }

        //Character 3

        if (character3.text == "Tank")
        {
            StorageList[2] = 1;
        }
        if (character3.text == "Healer")
        {
            StorageList[2] = 2;
        }
        if (character3.text == "Sword")
        {
            StorageList[2] = 3;
        }
        if (character3.text == "Sniper")
        {
            StorageList[2] = 4;
        }

        //Character 4

        if (character4.text == "Tank")
        {
            StorageList[3] = 1;
        }
        if (character4.text == "Healer")
        {
            StorageList[3] = 2;
        }
        if (character4.text == "Sword")
        {
            StorageList[3] = 3;
        }
        if (character4.text == "Sniper")
        {
            StorageList[3] = 4;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
