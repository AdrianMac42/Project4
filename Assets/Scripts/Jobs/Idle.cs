using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Job {


    GameObject[] objs;
    CharacterStats character;
    int noInTeam;

    // Use this for initialization
    void Start () {

        title = "Idle";
        numberOfWorkers1 = 0;
        numberOfWorkers2 = 0;
        priority = 0;
        totalJobProduction = 0;
    }
	
	// Update is called once per frame
	void Update ()
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

    void work(CharacterStats c)
    {
        strole(c);
    }
    void strole(CharacterStats c)
    {
        if (c.currentWorkTarget == null)
        {
            c.currentWorkTarget = new GameObject();
            c.currentWorkTarget.transform.position = c.transform.position;
        }
        int ran = UnityEngine.Random.Range(1, 60);
        if (ran == 1)
        {
            c.currentWorkTarget.transform.position += new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0);
        }

        c.transform.position = Vector3.MoveTowards(c.transform.position, c.currentWorkTarget.transform.position, (c.speed * Time.smoothDeltaTime));
    }
}
