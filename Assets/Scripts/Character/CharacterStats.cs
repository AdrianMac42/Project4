using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
     
    public int maxMorale;
    public int currentMorale;
     
    public int maxCarryLoad;
    public int currentCarryLoad;

    public int viewRange;
    public int speed;
    public int attackSpeed;
    public int armorClass;
     
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
     
    public int gathering;
    public int building;
    public int crafting;
    public int hunting;
    public int research;
    public int healing;
    public int animalHandling;
    public int inspiration;
     
    public Team team;
    public int teamNo;

    //JOB STUFF
    public Job currentJob;
    public GameObject[] workTargets;
    public GameObject currentWorkTarget;
    public GameObject[] stockpiles;
    public GameObject targetStockpile;
    public bool collected = false;
    public bool butchering = false;


    public Action currentAction;
    
    public AiStrategy aiStrategy;
     
    public Item lHand;
    public Item rHand;
    public Armor armor;

    public void equipLeft(Item item)
    {
        lHand = item;
    }

    public void equipRight(Item item)
    {
        rHand = item;
    }

    public void equipArmor(Armor armr)
    {
        armor = armr;
    }

    public void takeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void healDamage(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    public void newCharacterStats(Team newTeam)
    {
        strength = 5;
        dexterity = 5;
        constitution = 5;
        intelligence = 5;
        wisdom = 5;
        charisma = 5;
        team = newTeam;
        aiStrategy = null;
        setStats();
    }

    //CONSTRUCTOR 
    public void newCharacterStats(Team newTeam, AiStrategy ai)
    {
        strength = 5;
        dexterity = 5;
        constitution = 5;
        intelligence = 5;
        wisdom = 5;
        charisma = 5;
        team = newTeam;
        aiStrategy = ai;
        setStats();
    }
    JobBasedAi job;
    Woodcutter w;
    Stonecutter s;
    AnimalTamer a;
    Crafter c;
    Job j;
    
    public void setStats()
    {
        w= GameObject.Find("JobManager").GetComponent<Woodcutter>();
        s= GameObject.Find("JobManager").GetComponent<Stonecutter>();
        a= GameObject.Find("JobManager").GetComponent<AnimalTamer>();
        c= GameObject.Find("JobManager").GetComponent<Crafter>();
        if (teamNo == 1)
        {
            if (GameObject.Find("JobManager").GetComponent<Woodcutter>().numberOfWorkers1 < 2)
            {
                j = w;
            }
            else if (GameObject.Find("JobManager").GetComponent<Stonecutter>().numberOfWorkers1 < 2)
            {
                j = s;
            }
            else if (GameObject.Find("JobManager").GetComponent<AnimalTamer>().numberOfWorkers1 < 2)
            {
                j = a;
            }
            else if (GameObject.Find("JobManager").GetComponent<Crafter>().numberOfWorkers1 < 2)
            {
                j = c;
            }
            else
            {
                j = w;
            }
        }
        else
        {
            if (GameObject.Find("JobManager").GetComponent<Woodcutter>().numberOfWorkers2 < 2)
            {
                j = w;
            }
            else if (GameObject.Find("JobManager").GetComponent<Stonecutter>().numberOfWorkers2 < 2)
            {
                j = s;
            }
            else if (GameObject.Find("JobManager").GetComponent<AnimalTamer>().numberOfWorkers2 < 2)
            {
                j = a;
            }
            else if (GameObject.Find("JobManager").GetComponent<Crafter>().numberOfWorkers2 < 2)
            {
                j = c;
            }
            else
            {
                j = w;
            }
        }
        viewRange = 50;
        double x = (dexterity / 2);
        speed = 10;
        //TEMP AI STRAT
        aiStrategy = job;
        currentJob = j;

        //HEALTH
        maxHealth = constitution * 8;
        currentHealth = maxHealth;
        //MORALE
        maxMorale = charisma * 8;
        currentMorale = maxMorale;
        //CARRYLOAD
        maxCarryLoad = strength * 2;
        currentCarryLoad = 0;

        //GATHERING
        gathering = strength;
        //BUILDING
        x = (dexterity + strength) / 2;
        building = (int)Math.Floor(x);
        //CRAFTING
        x = (dexterity + intelligence) / 2;
        crafting = (int)Math.Floor(x);
        //HUNTING
        x = (dexterity + wisdom) / 2;
        hunting = (int)Math.Floor(x);
        //RESEARCH
        research = intelligence;
        //HEALING
        x = (intelligence + wisdom) / 2;
        healing = (int)Math.Floor(x);
        //ANIMALHANDLING
        x = (charisma + wisdom) / 2;
        animalHandling = (int)Math.Floor(x);
        //INSPIRATION
        inspiration = charisma;
    }
}
