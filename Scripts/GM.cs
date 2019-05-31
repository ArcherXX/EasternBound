using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public int Turn;
    public readonly GameObject[] units;
    public int[] speedList;
    public int[] TurnOrder;
    public int ticktockafuckingclock;
    // Use this for initialization
    public void combat()
    {
        speedList = new int[8];
        TurnOrder = new int[9];
        for(int i = 1; i < 5; i++)
        {
            speedList[i-1] = GameObject.Find("Ally#" + i).GetComponent<UnitScript>().speed + Random.Range(1,8);
        }
        for(int i = 1; i < 5; i++)
        {
            speedList[i + 3] = GameObject.Find("Enemy#" + i).GetComponent<UnitScript>().speed + Random.Range(1,8);
        }

        for(int i = 0; i < 8; i++)
        {
            for(int o = 0; o < 8; o++)
            {
                if(o==0)
                {
                    TurnOrder[i] = o;
                }else if(speedList[TurnOrder[i]] < speedList[o])
                {
                    TurnOrder[i] = o;
                }
            }
            Debug.Log(speedList[TurnOrder[i]] + " object number " + TurnOrder[i]);
            speedList[TurnOrder[i]] = -20;
        }
        Turn = 0;
        nextTurn();
    }
    public int gameCondition()
    {
        bool win = true;
        bool lose = true;
        for(int i = 1; i<5; i++)
        {
            if (GameObject.Find("Ally#" + i).GetComponent<UnitScript>().dead == false)
            {
                lose = false;
            }
            if (GameObject.Find("Enemy#" + i).GetComponent<UnitScript>().dead == false)
            {
                win = false;
            }

        }
        if (win)
        {
            return 1;
        }
        else if (lose)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
    public void nextTurn()
    {
        Debug.Log("Starting next turn");
        if (gameCondition() == 1)
        {
            Debug.Log("You won, bitch");
        }
        else if(gameCondition() == 2)
        {
            Debug.Log("You lost, fucker");
        }
        else if (TurnOrder[Turn] == 7) combat();
        else
        {
            int Cheezwiz = TurnOrder[Turn];
            Turn++;
            Debug.Log("Unit#" + Cheezwiz + " processing");
            if (Cheezwiz <= 3)
            {
                int allyTurn = Cheezwiz + 1;
                Debug.Log("Ally#" + allyTurn + " starting turn");
                GameObject refAlly = GameObject.Find("Ally#"+allyTurn);
                refAlly.GetComponent<UnitScript>().StartTurn();
            }
            else
            {
                int enemyTurn = Cheezwiz - 3;
                Debug.Log("Enemy#" + enemyTurn + " starting turn");
                GameObject.Find("Enemy#" + enemyTurn).GetComponent<UnitScript>().StartTurn();
            }
            
        }
        
    }

    void Start () {
        Debug.Log("HELLO MUTHERFOKCER");
        ticktockafuckingclock = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (ticktockafuckingclock >= 0)
        {
            ticktockafuckingclock--;
        }
        if (ticktockafuckingclock == 0)
        {
            combat();
        }
	}
    
}
