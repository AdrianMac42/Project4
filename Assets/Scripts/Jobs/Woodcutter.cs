using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : Job {

    GameObject[] objs;
    CharacterStats character;
    void Start ()
    {
        title = "Woodcutter";
        numberOfWorkers1 = 0;
        numberOfWorkers2 = 0;
        priority = 0;
        totalJobProduction = 0;
    }

    void Update()
    {
        numberOfWorkers1 = 0;
        numberOfWorkers2 = 0;
        objs = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject element in objs)
        {
            if (element.GetComponent<CharacterStats>().currentJob.title == title)
            {
                if (element.GetComponent<CharacterStats>().teamNo == 1)
                    numberOfWorkers1++;
                else
                    numberOfWorkers2++;
                work(element.GetComponent<CharacterStats>());
            }
        }
    }

    void work(CharacterStats ch)
    {
        if (ch.teamNo == 1)
        {
            ch.targetStockpile = GameObject.Find("Team1Spawn");
        }
        else if (ch.teamNo == 2)
        {
            ch.targetStockpile = GameObject.Find("Team2Spawn");
        }
        else
        {
            Debug.LogError("STOCKPILE NOT ASSIGNED");
        }
        ch.workTargets = GameObject.FindGameObjectsWithTag("Tree");
        if (ch.currentCarryLoad < ch.maxCarryLoad && ch.workTargets.Length >= 1)
        {
            foreach (GameObject tar in ch.workTargets)
            {
                if (ch.currentWorkTarget == null)
                {
                    ch.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, ch.transform.position)) 
                    < (Vector3.Distance(ch.currentWorkTarget.transform.position, ch.transform.position)))
                {
                    ch.currentWorkTarget = tar;
                }
            }

            // if theres a target within range
            if (ch.currentWorkTarget)
            {
                Vector3 targetLoc = (ch.currentWorkTarget.transform.position - ch.transform.position);
                // if not within range...move to it
                if (Vector3.Distance(ch.transform.position, ch.currentWorkTarget.transform.position) >= 1)
                {
                    //ch.transform.position += targetLoc * ch.speed * Time.deltaTime;
                    //ch.GetComponent<Rigidbody>().AddForce(targetLoc * ch.speed * Time.smoothDeltaTime);
                    float step = ch.speed * Time.deltaTime;
                    ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.currentWorkTarget.transform.position, (ch.speed * Time.smoothDeltaTime));
                }
                else // if in range...cut
                {
                    ch.currentWorkTarget.GetComponent<Trees>().wood -= 1;
                    ch.currentCarryLoad += 1;
                }
            }
        }
        else if (ch.currentCarryLoad > 0)
        {

            Vector3 targetLoc = (ch.targetStockpile.transform.position - ch.transform.position);
            // if not withing range...move to it
            if (Vector3.Distance(ch.transform.position, ch.targetStockpile.transform.position) >= 0.5f)
            {
                ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetStockpile.transform.position, (ch.speed * Time.smoothDeltaTime));
            }
            else // if in range...deposit
            {
                ch.targetStockpile.GetComponent<Stockpile>().wood += ch.currentCarryLoad;
                ch.currentCarryLoad -= ch.currentCarryLoad;
            }

        }
    }





}
