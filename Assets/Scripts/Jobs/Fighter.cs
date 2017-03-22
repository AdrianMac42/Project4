using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Job {

    GameObject[] objs;
    CharacterStats character;
    void Start ()
    {
        title = "Fighter";
        numberOfWorkers1 = 0;
        numberOfWorkers2 = 0;
        priority = 0;
        totalJobProduction = 0;
    }

    void Update()
    {
        //numberOfWorkers1 = 0;
        //numberOfWorkers2 = 0;
        //objs = GameObject.FindGameObjectsWithTag("Character");
        //foreach (GameObject element in objs)
        //{
        //    if (element.GetComponent<CharacterStats>().currentJob.title == title)
        //    {
        //        if (element.GetComponent<CharacterStats>().teamNo == 1)
        //            numberOfWorkers1++;
        //        else
        //            numberOfWorkers2++;
        //        work(element.GetComponent<CharacterStats>());
        //    }
        //}
    }

    public void fight(CharacterStats ch)
    {
        // get all targets
        // find closest
        // if have bow && bow has ammo
        //  if enemy in range, attack
        //  else move to enemy
        // else if have sword
        //  if in range, attack
        //  else move to target
        if (ch.teamNo == 1)
        {
            ch.enemies = GameObject.FindGameObjectsWithTag("Team2");
        }
        else if (ch.teamNo == 2)
        {
            ch.enemies = GameObject.FindGameObjectsWithTag("Team1");
        }
        else
        {
            Debug.LogError("NO ENEMIES");
        }

        foreach(GameObject en in ch.enemies)
        {
            CharacterStats enemy = en.GetComponent<CharacterStats>();
            if(ch.targetEnemy == null)
            {
                ch.targetEnemy = enemy;
            }
            else if(Vector3.Distance(ch.transform.position, enemy.transform.position) 
                < Vector3.Distance(ch.transform.position, ch.targetEnemy.transform.position))
            {
                ch.targetEnemy = enemy;
            }
        }

        if(ch.bow != null && ch.bow.ammo > 0)
        {
            if (Vector3.Distance(ch.transform.position, ch.targetEnemy.transform.position) <= ch.bow.range )
            {
                ch.bow.attack(ch, ch.targetEnemy);
            }
            else
            {
                ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetEnemy.transform.position, (ch.speed * Time.smoothDeltaTime));
            }
        }
        else if (ch.sword != null)
        {
            if (Vector3.Distance(ch.transform.position, ch.targetEnemy.transform.position) <= ch.sword.range)
            {
                ch.sword.attack(ch, ch.targetEnemy);
            }
            else
            {
                ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetEnemy.transform.position, (ch.speed * Time.smoothDeltaTime));
            }
        }
        else if (ch.dagger != null)
        {
            if (Vector3.Distance(ch.transform.position, ch.targetEnemy.transform.position) <= ch.dagger.range)
            {
                ch.dagger.attack(ch, ch.targetEnemy);
            }
            else
            {
                ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetEnemy.transform.position, (ch.speed * Time.smoothDeltaTime));
            }
        }
        else if (ch.unarmed != null)
        {
            if (Vector3.Distance(ch.transform.position, ch.targetEnemy.transform.position) <= ch.unarmed.range)
            {
                ch.unarmed.attack(ch, ch.targetEnemy);
            }
            else
            {
                ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetEnemy.transform.position, (ch.speed * Time.smoothDeltaTime));
            }
        }
        else
        {
            Debug.LogError("NO WEAPONS");
        }

    }


    void melee(CharacterStats ch)
    {

    }

    void ranged(CharacterStats ch)
    {

    }




}
