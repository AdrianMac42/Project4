using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTamer : Job {

    GameObject[] objs;
    CharacterStats character;
    int animalLimit;
    int animals;
    GameObject[] targets;

    void Start ()
    {
        title = "AnimalTamer";
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
        animalLimit = ch.animalHandling;
        if (animals < animalLimit && ch.butchering == false)
        {
            targets = GameObject.FindGameObjectsWithTag("Cow");
            foreach (GameObject cow in targets)
            {
                if(!cow.GetComponent<Cows>().tamed )
                {
                    if (ch.currentWorkTarget == null)
                    {
                        ch.currentWorkTarget = cow;
                    }
                    else if (Vector3.Distance(ch.transform.position, ch.currentWorkTarget.transform.position) > 
                        Vector3.Distance(cow.transform.position, ch.currentWorkTarget.transform.position))
                    {
                        ch.currentWorkTarget = cow;
                    }
                }
            }
        }
        else 
        {
            targets = GameObject.FindGameObjectsWithTag("Cow");
            foreach (GameObject cow in targets)
            {
                Cows c = cow.GetComponent<Cows>();
                if (c.food >= c.maxFood)
                {
                    ch.currentWorkTarget = cow;
                    ch.butchering = true;
                    c.alive = false;
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
                        ch.currentWorkTarget.GetComponent<Stones>().stone -= 1;
                        ch.currentCarryLoad += 1;
                    }
                }
            }
        }
    }
}
