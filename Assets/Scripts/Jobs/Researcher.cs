using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Researcher : Job {

    GameObject[] objs;
    CharacterStats character;
    void Start ()
    {
        title = "Researcher";
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

    }





}
