using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitScript : MonoBehaviour
{
    //----------------------------Attack Code Here-------------------------------------------
    public class attack
    {
        public double multiplier;
        public int type;//0 = attack, 1 = heal
        public int stun;
        public int hit;
        public int[] hitPosition;//0 = NA, 1 = valid target, 2 = group targets
        public string attackName;
        //-------------------------Attack Creation------------------------------------------------------
        public attack(string namer, int T,double m, int hitRate,int stunny,  int p1, int p2, int p3, int p4)
        {
            stun = stunny;
            type = T;
            if (T == 0)
            {
                multiplier = m / 100;
            }
            else { multiplier = m; }
            hit = hitRate;
            attackName = namer;
            hitPosition = new int[4];
            hitPosition[0] = p1;
            hitPosition[1] = p2;
            hitPosition[2] = p3;
            hitPosition[3] = p4;
        }
        //---------------------------When Used by an Ally----------------------------------------------
        public void AllyUse(int baseMin, int baseMax, int crit)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Target");
            int targetPosition = target.GetComponent<UnitScript>().positionInt - 1;
            //Random hitRoll = new Random();
            int damage = 0;
            double res = target.GetComponent<UnitScript>().armor/100;
            if (hitPosition[targetPosition] == 1)
            {
                if (hit >= Random.Range(1, 100))
                {
                    if (crit >= Random.Range(1, 100))
                    {
                        damage = (int)(Random.Range(baseMin, baseMax) * multiplier * 3 * (1 - res));
                        if(stun > 0)
                        {
                            Debug.Log("stun successful");
                            target.GetComponent<UnitScript>().stunned = true;
                        }
                    }
                    else
                    {
                        damage = (int)(Random.Range(baseMin, baseMax) * multiplier * (1 - res));
                        if (Random.Range(1, 100) <= stun)
                        {
                            target.GetComponent<UnitScript>().stunned = true;
                            Debug.Log("stun successful");
                        }
                    }
                }
                target.GetComponent<UnitScript>().takeDamage(damage);
            }
            else if(hitPosition[targetPosition] == 2)
            {
                target.GetComponent<UnitScript>().retag();
                for(int i = 1; i < 5; i++)
                {
                    if(hitPosition[i-1] == 2)
                    {
                        GameObject TempTarget = GameObject.Find("Enemy#" + i);
                        res = TempTarget.GetComponent<UnitScript>().armor / 100;
                        if(hit >= Random.Range(1, 100))
                        {
                            if (crit >= Random.Range(1, 100))
                            {
                                damage = (int)(Random.Range(baseMin, baseMax) * multiplier * 3 * (1 - res));
                                if (stun > 0)
                                {
                                    TempTarget.GetComponent<UnitScript>().stunned = true;
                                }
                            }
                            else
                            {
                                damage = (int)(Random.Range(baseMin, baseMax) * multiplier * (1 - res));
                                if (Random.Range(1, 100) <= stun)
                                {
                                    TempTarget.GetComponent<UnitScript>().stunned = true;
                                }
                            }
                        }
                        TempTarget.GetComponent<UnitScript>().takeDamage(damage);
                    }
                }
            }
        }
        //----------------------------When Used by an Enemy------------------------------------------------
        public void EnemyUse(int baseMin, int baseMax)
        {
            int Rand = Random.Range(1, 4);
            while(hitPosition[Rand] == 0)
            {
                Rand = Random.Range(1, 4);
            }
            GameObject target = GameObject.FindGameObjectWithTag("Ally" + Rand);
            int damage = 0;
            if (hit >= Random.Range(1, 100))
            {
                damage = (int)(Random.Range(baseMin, baseMax)*multiplier);
            }
            target.GetComponent<UnitScript>().takeDamage(damage);
            //GameObject.Find("GameManager").GetComponent<GM>().nextTurn();
        }
        //------------------------------------When Used by an Ally HEALING------------------------------------
        public void AllyHeal(int baseMin, int baseMax)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Target");
            int targetPosition = target.GetComponent<UnitScript>().positionInt - 1;
            int heal = (int)(Random.Range(baseMin, baseMax)*multiplier);
            if (hitPosition[targetPosition]==1)
            {
                target.GetComponent<UnitScript>().getHealed(heal);
            }
            else if (hitPosition[targetPosition] == 2)
            {
                for(int i = 1;i<5; i++)
                {
                    if (hitPosition[i - 1] == 2)
                    {
                        GameObject TempTarget = GameObject.FindGameObjectWithTag("Ally" + i);
                        TempTarget.GetComponent<UnitScript>().getHealed(heal);
                    }
                }
            }
        }
    }


    //------------------------------------------receive healing---------------------------------------------
    public void getHealed(int ammount)
    {
        this.health += ammount;
        if(this.health > maxHealth)
        {
            health = maxHealth;
        }
    }



    //----------------------------------------Unit Class Code Here---------------------------------------
    public class unitType
    {
        public string name;
        public int healthMax;
        public int minDamage;
        public int maxDamage;
        public int armor;
        public int crit;
        public string animator;
        public attack[] attacks;
        //--------------------------------------Class Creation-----------------------------------------------
        public unitType(string namer, int minD, int maxD, int crit, int healthiness, int tank, string animations, attack attack1, attack attack2, attack attack3, attack attack4)
        {
            attacks = new attack[10];
            name = namer;
            healthMax = healthiness;
            armor = tank;
            minDamage = minD;
            maxDamage = maxD;
            attacks[0] = attack1;
            attacks[1] = attack2;
            attacks[2] = attack3;
            attacks[3] = attack4;
            animator = animations;
        }
    }

    public void getStunned()
    {
        this.stunned = true;
    }




    public bool stunned;
    public int armor;
    public int health;
    public int positionInt;
    public int ClassInt;
    public int minD;
    public int maxD;
    public int critChance;
    public int speed;
    public bool dead;
    public bool selected;
    public bool Ally;
    public unitType[] UnitClass;
    
    
    //----------------------------------------------------------Start Turn Function-------------------------------------------------------------------
    public void StartTurn()
    {
        if (dead)
        {
            GameObject.Find("GameManager").GetComponent<GM>().nextTurn();
        }
        else if (stunned)
        {
            stunned = false;
            selected = false;
            GameObject.Find("GameManager").GetComponent<GM>().nextTurn();
        }
        else if (Ally)
        {
            selected = true;
            this.tag = "Selected";
            GameObject.Find(this.name + "Canvas").GetComponent<Canvas>().sortingOrder = 1;
            //GameObject.Find(this.name + "Canvas").SetActive(true);
            /*GameObject refCanvas = GameObject.Find(this.name + "Canvas");
            refCanvas.SetActive(true);*/


            /*Debug.Log("Ally Turn, Setting Buttons");
            for (int i = 1; i < 3; i++) {
                GameObject.Find("Attack " + i).GetComponentInChildren<Text>().text = UnitClass[ClassInt].attacks[i-1].name;
            }*/
        }
        else
        {
            Debug.Log("Enemy Turn, Using Attack");
            int chosen = Random.Range(1, 4);
            if (Classes[ClassInt].attacks[chosen].type == 0)
            {
                Classes[ClassInt].attacks[chosen].EnemyUse(minD, maxD);
            }
            else
            {
                //Classes[ClassInt].attacks[chosen].EnemyHeal(maxHealth);
            }
            GameObject.Find("GameManager").GetComponent<GM>().nextTurn();
        }
    }


    //--------------------------------------------------------------Take Damage Function---------------------------------------------------------------
    public Vector3 dsymbolposition;

    public void takeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " health= " + health);
        if (dead == false)
        {
            if (Ally)
            {
                dsymbolposition = new Vector3(-2 * positionInt, .5f, -1);
            }
            else
            {
                dsymbolposition = new Vector3(2 * positionInt, 0.5f, -1);
            }
            Instantiate(damagesymbol, dsymbolposition, transform.rotation);
        }
        if (health <= 0)
        {
            Debug.Log(name + " is dead");
            dead = true;
            transform.Rotate(new Vector3(0, 0, 90));
        }
    }




    //------------------------------------------------Use Attack Function------------------------------------------------------------------
    public Transform damagesymbol;

    public void useAttack(int attackNumber)
    {
        Debug.Log("preparing to attack");
        selected = false;
        Debug.Log(this.name + " using attack #" + attackNumber);
        retag();
        Debug.Log(minD);
        Debug.Log(maxD);
        Debug.Log(critChance);
        Debug.Log(this.ClassInt);
        Debug.Log(attackNumber);
        Debug.Log(Classes[this.ClassInt].name);
        
        if (Classes[this.ClassInt].attacks[attackNumber].type == 0)//attack
        {
            Classes[this.ClassInt].attacks[attackNumber].AllyUse(minD, maxD, critChance);
        }
        else if (Classes[this.ClassInt].attacks[attackNumber].type == 1)//healing
        {
            GameObject[] targets;
            targets = GameObject.FindGameObjectsWithTag("Target");
            if(targets.Length == 0)
            {
                this.target();
            }
            Classes[this.ClassInt].attacks[attackNumber].AllyHeal(minD, maxD);
        }
        GameObject.Find(this.name + "Canvas").GetComponent<Canvas>().sortingOrder = 0;
        transform.localScale = new Vector3(7, 7, 1);
        Debug.Log("passing turn");
        GameObject.Find("GameManager").GetComponent<GM>().nextTurn();
    }



    //-------------------------------------------------Swap Position Function--------------------------------------------------------------
    public void swapPosition()
    {
        bool run = GameObject.FindGameObjectWithTag("Target").GetComponent<UnitScript>().Ally;
        if (run)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Target");
            int PosA = this.positionInt;
            int PosB = target.GetComponent<UnitScript>().positionInt;
            Debug.Log("Position Swap Function Running");
            Debug.Log("PosA = " + PosA);
            Debug.Log("PosB = " + PosB);

            this.setPosition(PosB);
            target.GetComponent<UnitScript>().setPosition(PosA);
            this.retag();
            target.GetComponent<UnitScript>().retag();
            GameObject.Find("GameManager").GetComponent<GM>().nextTurn();
        }
    }

    public void target()
    {
        this.tag = "Target";
    }

    public void setPosition(int pos)
    {
        this.positionInt = pos;
    }


    //-----------------------------------------------------resets tag---------------------------------------------------------
    public void retag()
    {
        this.tag = "Ally" + this.positionInt;
    }

    public GameObject storage;
    public CharacterSelectStorage charstorage;

    public GameObject charactersavehealth;
    public CharacterSavedHealth healthscript;

    private int[] healths;
    private int[] armors;
    private int[] mins;
    private int[] maxs;
    private int[] speeds;
    private int[] crits;
    private string[] classNames;
    private string[] spriteSheets;
    private int[] attackID;
    private TextAsset[] attackSheets;
    public TextAsset Classlist;
    public TextAsset AttackSheet;
    public unitType[] Classes;
    private attack[] attacks;
    private int type;
    private int damageMod;
    private int accuracy;
    private int stun;
    private int[] validTargets;
    void loadClasses()
    {
        Classes = new unitType[10];
        healths = new int[10];
        armors = new int[10];
        mins = new int[10];
        maxs = new int[10];
        speeds = new int[10];
        crits = new int[10];
        classNames = new string[10];
        spriteSheets = new string[10];
        attackID = new int[10];
        Debug.Log(Classlist.text);
        string[] data = Classlist.text.Split(new char[] { '\n' });
        for(int i = 1; i < data.Length-1; i++)
        {
            string[] ClassRow = data[i].Split(new char[] { ',' });
            classNames[i] = ClassRow[1];
            int.TryParse(ClassRow[2], out maxs[i]);
            int.TryParse(ClassRow[3], out mins[i]);
            int.TryParse(ClassRow[4], out crits[i]);
            int.TryParse(ClassRow[5], out healths[i]);
            int.TryParse(ClassRow[6], out armors[i]);
            spriteSheets[i] = ClassRow[7];
            int.TryParse(ClassRow[8], out attackID[i]);
            attacks = new attack[4];
            string[] attackRow = AttackSheet.text.Split(new char[] { '\n' });
            for(int p = 0; p<4; p++)
            {
                
                string[] attackData = attackRow[p + 2+ 5*attackID[i]].Split(new char[] { ',' });

                validTargets = new int[4];
                string attackName = attackData[1];
                int.TryParse(attackData[2], out type);
                int.TryParse(attackData[3], out damageMod);
                int.TryParse(attackData[4], out accuracy);
                int.TryParse(attackData[5], out stun);
                for(int o = 0; o < 4; o++)
                {
                    int.TryParse(attackData[6 + o], out validTargets[o]);
                }
                attacks[p] = new attack(attackName, type,damageMod, accuracy, stun, validTargets[0], validTargets[1], validTargets[2], validTargets[3]);
            }
            Classes[i-1] = new unitType(classNames[i], mins[i], maxs[i], crits[i], healths[i], armors[i], spriteSheets[i], attacks[0], attacks[1], attacks[2], attacks[3]);

        }

    }

    void loadAttacks()
    {

    }
    public int roundswon = 0;
    public GameObject unit;
    public RoundCounter uscript;
    public int maxHealth;
    void Start()
    {
        charactersavehealth = GameObject.Find("CharacterHealthsaver");
        healthscript = charactersavehealth.GetComponent<CharacterSavedHealth>();

        unit = GameObject.Find("Round Counter");
        uscript = unit.GetComponent<RoundCounter>();
        roundswon = uscript.rounds;


        loadClasses();

        storage = GameObject.Find("SelectionStorage");
        charstorage = storage.GetComponent<CharacterSelectStorage>();

        dead = false;
        selected = false;
        maxHealth = Classes[ClassInt].healthMax;
        health = Classes[ClassInt].healthMax;
        armor = Classes[ClassInt].armor;
        minD = Classes[ClassInt].minDamage;
        maxD = Classes[ClassInt].maxDamage;
        critChance = Classes[ClassInt].crit;
        speed = Random.Range(0, 4);
        if (Ally)
        {
            GameObject.Find(this.name + "Canvas").transform.SetPositionAndRotation(new Vector3(83, 1000, 0), Quaternion.identity);
        }

        for (int i = 1; i < 5; i++)
        {
            if (this.name == "Ally#" + i)
            {
                positionInt = i;
                this.tag = "Ally" + i;
            }
            else if (this.name == "Enemy#" + i)
            {
                positionInt = i;
                this.tag = "Enemy" + i;
            }

        }

        if (this.tag == "Ally1")
        {
            if (charstorage.StorageList[0] == 1)
            {
                ClassInt = 0;
            }
            if (charstorage.StorageList[0] == 2)
            {
                ClassInt = 1;
            }
            if (charstorage.StorageList[0] == 3)
            {
                ClassInt = 2;
            }
            if (charstorage.StorageList[0] == 4)
            {
                ClassInt = 3;
            }
        }

        if (this.tag == "Ally2")
        {
            if (charstorage.StorageList[1] == 1)
            {
                ClassInt = 0;
            }
            if (charstorage.StorageList[1] == 2)
            {
                ClassInt = 1;
            }
            if (charstorage.StorageList[1] == 3)
            {
                ClassInt = 2;
            }
            if (charstorage.StorageList[1] == 4)
            {
                ClassInt = 3;
            }
        }

        if (this.tag == "Ally3")
        {
            if (charstorage.StorageList[2] == 1)
            {
                ClassInt = 0;
            }
            if (charstorage.StorageList[2] == 2)
            {
                ClassInt = 1;
            }
            if (charstorage.StorageList[2] == 3)
            {
                ClassInt = 2;
            }
            if (charstorage.StorageList[2] == 4)
            {
                ClassInt = 3;
            }
        }

        if (this.tag == "Ally4")
        {
            if (charstorage.StorageList[3] == 1)
            {
                ClassInt = 0;
            }
            if (charstorage.StorageList[3] == 2)
            {
                ClassInt = 1;
            }
            if (charstorage.StorageList[3] == 3)
            {
                ClassInt = 2;
            }
            if (charstorage.StorageList[3] == 4)
            {
                ClassInt = 3;
            }
        }

        updateClass();

        if (roundswon > 0)
        {
            if (this.name == "Ally#1")
            {
                health = healthscript.character1health;
            }
            if (this.name == "Ally#2")
            {
                health = healthscript.character2health;
            }
            if (this.name == "Ally#3")
            {
                health = healthscript.character3health;
            }
            if (this.name == "Ally#4")
            {
                health = healthscript.character4health;
            }
        }
    }

    void updateClass()
    {
        int percentHealth = (health / maxHealth) * 100;
        maxHealth = Classes[ClassInt].healthMax;
        health = Classes[ClassInt].healthMax * percentHealth / 100;
        armor = Classes[ClassInt].armor;
        minD = Classes[ClassInt].minDamage;
        maxD = Classes[ClassInt].maxDamage;
        critChance = Classes[ClassInt].crit;
    }

    // Update is called once per frame
    private void Update()
    {

        if (Ally)
        {

            transform.SetPositionAndRotation(new Vector3(-2 * positionInt, .5f, 0), Quaternion.identity);
            
            if (this.tag == "Selected")
            {
                transform.localScale = new Vector3(10,10,1);
                transform.SetPositionAndRotation(new Vector3(-2 * positionInt, .9f, 0), Quaternion.identity);
            }
            else if (this.tag == "Target")
            {
                transform.localScale = new Vector3(8, 8, 1);
            }
            else
            {
                transform.localScale = new Vector3(7, 7, 1);
                tag = "Ally" + positionInt;
            }
        }
        else
        {
            transform.SetPositionAndRotation(new Vector3(2 * positionInt, 0.5f, 0), new Quaternion(0, 180, 0, 0));
            if (this.tag == "Target")
            {
                transform.localScale = new Vector3(9, 9, 1);
                transform.SetPositionAndRotation(new Vector3(2 * positionInt, .9f, 0), new Quaternion(0, 180, 0, 0));
            }
            else
            {
                tag = "Enemy" + positionInt;
                transform.localScale = new Vector3(7, 7, 1);
            }
            
            
        }
        if (health <= 0)
        {
            Debug.Log(name + " is dead");
            dead = true;
            transform.Rotate(new Vector3(0, 0, 90));
        }
        if (dead)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Debug.Log(name + " Health:" + health.ToString());
        
    }
}
