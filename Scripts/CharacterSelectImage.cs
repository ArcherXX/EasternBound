using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectImage : MonoBehaviour {

    public GameObject storage;
    public CharacterSelectStorage charstorage;
    public Image imagecomponent;

    public Sprite Tank;
    public Sprite Healer;
    public Sprite Sword;
    public Sprite Sniper;

	// Use this for initialization
	void Start () {
        storage = GameObject.Find("SelectionStorage");
        charstorage = storage.GetComponent<CharacterSelectStorage>();
        imagecomponent = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.tag == "Ally1")
        {
            if(charstorage.StorageList[0] == 1)
            {
                imagecomponent.sprite = Tank;
            }
            if (charstorage.StorageList[0] == 2)
            {
                imagecomponent.sprite = Healer;
            }
            if (charstorage.StorageList[0] == 3)
            {
                imagecomponent.sprite = Sword;
            }
            if (charstorage.StorageList[0] == 4)
            {
                imagecomponent.sprite = Sniper;
            }
        }

        if (this.tag == "Ally2")
        {
            if (charstorage.StorageList[1] == 1)
            {
                imagecomponent.sprite = Tank;
            }
            if (charstorage.StorageList[1] == 2)
            {
                imagecomponent.sprite = Healer;
            }
            if (charstorage.StorageList[1] == 3)
            {
                imagecomponent.sprite = Sword;
            }
            if (charstorage.StorageList[1] == 4)
            {
                imagecomponent.sprite = Sniper;
            }
        }

        if (this.tag == "Ally3")
        {
            if (charstorage.StorageList[2] == 1)
            {
                imagecomponent.sprite = Tank;
            }
            if (charstorage.StorageList[2] == 2)
            {
                imagecomponent.sprite = Healer;
            }
            if (charstorage.StorageList[2] == 3)
            {
                imagecomponent.sprite = Sword;
            }
            if (charstorage.StorageList[2] == 4)
            {
                imagecomponent.sprite = Sniper;
            }
        }

        if (this.tag == "Ally4")
        {
            if (charstorage.StorageList[3] == 1)
            {
                imagecomponent.sprite = Tank;
            }
            if (charstorage.StorageList[3] == 2)
            {
                imagecomponent.sprite = Healer;
            }
            if (charstorage.StorageList[3] == 3)
            {
                imagecomponent.sprite = Sword;
            }
            if (charstorage.StorageList[3] == 4)
            {
                imagecomponent.sprite = Sniper;
            }
        }
    }
}
