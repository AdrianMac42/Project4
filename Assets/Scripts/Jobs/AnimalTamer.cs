using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTamer : Job {

    GameObject[] objs;
    CharacterStats character;
    GameObject[] targets;
    string team1Tag = "Cow1";
    string team2Tag = "Cow2";
    string tag;
    int untamedCows;

    void Start ()
    {
        title = "AnimalTamer";
        numberOfWorkers1 = 0;
        numberOfWorkers2 = 0;
        priority = 0;
        totalJobProduction = 0;
        untamedCows = GameObject.FindGameObjectsWithTag("Cow").Length;
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
                {
                    numberOfWorkers1++;
                    tag = team1Tag;
                }
                else
                {
                    numberOfWorkers2++;
                    tag = team2Tag;
                }
                if (GameObject.Find("GameController").GetComponent<PhaseManager>().phaseNo <= 1)
                {
                    work(element.GetComponent<CharacterStats>());
                }
                else
                {
                    finishWork(element.GetComponent<CharacterStats>());
                }
            }
        }
    }


    void finishWork(CharacterStats ch)
    {
        if (ch.currentCarryLoad > 0)
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
            // if not withing range...move to it
            if (Vector3.Distance(ch.transform.position, ch.targetStockpile.transform.position) >= 0.5f)
            {
                ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetStockpile.transform.position, (ch.speed * Time.smoothDeltaTime));
            }
            else // if in range...deposit
            {
                if (ch.butchering == true)
                {
                    ch.targetStockpile.GetComponent<Stockpile>().food += ch.currentCarryLoad;
                    ch.currentCarryLoad -= ch.currentCarryLoad;
                }
                else
                {
                    ch.targetStockpile.GetComponent<Stockpile>().leather += ch.currentCarryLoad;
                    ch.currentCarryLoad -= ch.currentCarryLoad;
                }
            }
        }
        else
        {
            ch.armUp();
        }
    }

    void work(CharacterStats ch)
    {
        if (ch.collected == false)
        {
            ch.animalLimit = ch.animalHandling;
            GameObject[] tCows = GameObject.FindGameObjectsWithTag(tag);
            int a = 0;
            foreach (GameObject cow in tCows)
            {
                if (cow.GetComponent<Cows>().tamer == ch.gameObject)
                {
                    a++;
                }
            }
            ch.animals = a;
            // if hes not butchering or skinning and he has room for more cows
            if (untamedCows >= 1 && ch.animals < ch.animalLimit && ch.butchering == false && ch.skinning == false)
            {
                // find cows
                targets = GameObject.FindGameObjectsWithTag("Cow");
                untamedCows = targets.Length;
                ch.currentWorkTarget = null;
                if (targets.Length >= 1)
                {
                    // find the closest one
                    foreach (GameObject cow in targets)
                    {
                        if (ch.currentWorkTarget == null)
                        {
                            ch.currentWorkTarget = cow;
                        }
                        else if (Vector3.Distance(ch.transform.position, ch.currentWorkTarget.transform.position) >
                            Vector3.Distance(ch.transform.position,cow.transform.position))
                        {
                            ch.currentWorkTarget = cow;
                        }

                    }
                    // go to it
                    if (Vector3.Distance(ch.transform.position, ch.currentWorkTarget.transform.position) >= 1)
                    {
                        ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.currentWorkTarget.transform.position, (ch.speed * Time.deltaTime));
                    }
                    // tame it
                    else
                    {
                        ch.currentWorkTarget.GetComponent<Cows>().tamer = ch.gameObject;
                        ch.currentWorkTarget.GetComponent<Cows>().tamed = true;
                    }
                }
            }
            // at animal limit and not skinning or butchering
            else if (ch.butchering == false && ch.skinning == false)
            {
                ch.currentWorkTarget = null;
                // find tamed cows
                targets = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject cow in targets)
                {
                    //check each cow to see if they have maximum food
                    Cows c = cow.GetComponent<Cows>();
                    // check if alive
                    if (c.alive == true)
                    {
                        if (c.food >= c.maxFood)
                        {
                            // if yes then butcher it
                            ch.currentWorkTarget = cow;
                            ch.butchering = true;
                            c.alive = false;
                        }
                    }
                    // if not check resources and set state 
                    else if (c.food > 0)
                    {
                        ch.butchering = true;
                        ch.skinning = false;
                        ch.currentWorkTarget = cow;
                    }
                    else if (c.leather > 0)
                    {
                        ch.butchering = false;
                        ch.skinning = true;
                        ch.currentWorkTarget = cow;
                    }
                }
                // If still not butchering or skinning, strole
                if (ch.butchering == false && ch.skinning == false)
                {
                    strole(ch);
                }
            }
            // if skinning
            else if (ch.skinning == true && ch.currentWorkTarget)
            {
                // if no leather left
                if (ch.currentWorkTarget.GetComponent<Cows>().leather <= 0)
                {
                    ch.collected = true;
                }
                // if leather left
                else
                {
                    // if not within range...move to it
                    if (Vector3.Distance(ch.transform.position, ch.currentWorkTarget.transform.position) >= 1)
                    {
                        ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.currentWorkTarget.transform.position, (ch.speed * Time.deltaTime));
                    }
                    else // if in range...cut
                    {
                        // gather leather
                        ch.currentWorkTarget.GetComponent<Cows>().leather -= 1;
                        ch.currentCarryLoad += 1;
                    }
                }
            }
            // if butchering
            else if (ch.butchering == true && ch.currentWorkTarget)
            {
                // if no food left
                if (ch.currentWorkTarget.GetComponent<Cows>().food <= 0)
                {
                    ch.collected = true;
                }
                // if food left
                else
                {
                    // if not within range...move to it
                    if (Vector3.Distance(ch.transform.position, ch.currentWorkTarget.transform.position) >= 1)
                    {
                        ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.currentWorkTarget.transform.position, (ch.speed * Time.deltaTime));
                    }
                    else // if in range...cut
                    {
                        // gather food
                        ch.currentWorkTarget.GetComponent<Cows>().food -= 1;
                        ch.currentCarryLoad += 1;
                    }
                }
            }
            else
            {
                ch.collected = true;
            }
        }
        else if (ch.collected == true)
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
            // return what has been gathered
            if (ch.currentCarryLoad > 0)
            {
                
                if (Vector3.Distance(ch.transform.position, ch.targetStockpile.transform.position) >= 1)
                {
                    ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetStockpile.transform.position, (ch.speed * Time.deltaTime));
                }
                else if (ch.butchering == true)// if in range...cut
                {
                    // drop off food
                    ch.targetStockpile.GetComponent<Stockpile>().food += ch.currentCarryLoad;
                    ch.butchering = false;
                    ch.skinning = false;
                    ch.currentCarryLoad = 0;
                    ch.collected = false;
                }
                else if (ch.skinning == true)// if in range...cut
                {
                    // drop off food
                    ch.targetStockpile.GetComponent<Stockpile>().leather += ch.currentCarryLoad;
                    ch.butchering = false;
                    ch.skinning = false;
                    ch.currentCarryLoad = 0;
                    ch.collected = false;
                }
            }
            else
            {
                ch.collected = false;
            }
        }
    }


    void strole(CharacterStats c)
    {

        if (c.teamNo == 1)
        {
            c.targetStockpile = GameObject.Find("Team1Spawn");
        }
        else if (c.teamNo == 2)
        {
            c.targetStockpile = GameObject.Find("Team2Spawn");
        }
        else
        {
            Debug.LogError("STOCKPILE NOT ASSIGNED");
        }
        if (c.strolePos == null)
        {
            c.strolePos = c.targetStockpile.transform.position;
        }
        int ran = UnityEngine.Random.Range(1, 100);
        if (ran == 1)
        {
            c.strolePos = (c.targetStockpile.transform.position + new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0));
        }
        c.transform.position = Vector3.MoveTowards(c.transform.position, c.strolePos, (c.speed * Time.smoothDeltaTime));
    }

}
