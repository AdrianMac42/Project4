using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhaseManager : MonoBehaviour {



    public string phaseName;
    public int phaseNo;
    public GameObject g;
    GameObject[] chars;
    int ready = 0;
    int chNo = 0;

    void Start () {
        startPhase();

    }
	

	void Update () {
        GameObject.Find("Phase").GetComponent<Text>().text = phaseName;
        chars = GameObject.FindGameObjectsWithTag("Character");
        if (phaseNo == 2)
        {
            ready = 0;
            chNo = 0;
            foreach (GameObject c in chars)
            {
                chNo++;
                if (c.GetComponent<CharacterStats>().readyToFight == true)
                {
                    ready++;
                }
            }
            if (ready == chNo)
            {
                battlePhase();
            }
        }

    }

    public void phaseChanger()
    {
        g.SetActive(true);
    }

    public void startPhase()
    {
        phaseName = "";
        phaseNo = 0;
    }

    public void craftPhase()
    {
        phaseName = "Crafting Phase";
        phaseNo = 1;
    }

    public void armingPhase()
    {
        phaseName = "Arming Phase";
        phaseNo = 2;

        foreach (GameObject c in chars)
        {
            c.GetComponent<CharacterStats>().finishingWork = true;
        }
    }

    public void battlePhase()
    {

        phaseName = "Battle Phase";
        phaseNo = 3;
        GameObject[] characte = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject c in characte)
        {
            if (c.GetComponent<CharacterStats>().teamNo == 1)
            {
                c.tag = "Team1";
            }
            else if (c.GetComponent<CharacterStats>().teamNo == 2)
            {
                c.tag = "Team2";
            }
            else
            {
                Debug.LogError("Problem");
            }
        }
    }

}
