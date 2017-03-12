using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour {

    public string teamName;
    public int teamCount;
    public AiStrategy teamAi;
    int t;
    Vector3 pos;
    Vector3 sp;
    GameObject character;
    GameObject spawnN;
    public Material mat;
    Woodcutter w;
    Stonecutter s;
    AnimalTamer a;
    Crafter c;
    Job j;

    public void makeTeam(GameObject c, string name, int x)
    {
        character = c;
        t = x;
        teamName = name;
        teamCount = 8;
        spawn();

    }
    public void makeTeam(GameObject c, string name, AiStrategy ai, int x)
    {
        character = c;
        t = x;
        teamName = name;
        teamCount = 8;
        teamAi = ai;
        spawn();
    }

    void spawn()
    {
        if (t == 1)
        {
            spawnN = GameObject.Find("Team1Spawn");
            pos = spawnN.transform.position;
        }
        else if (t == 2)
        {
            spawnN = GameObject.Find("Team2Spawn");
            pos = spawnN.transform.position;
        }
        else
        {
            Debug.LogError("SPAWN NOT SELECTED");
        }
        for (int i = 0; i < teamCount; i++)
        {
            Vector3 ran = new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0);
            sp = pos + ran;
            GameObject chara = Instantiate(character, sp, spawnN.transform.rotation,this.gameObject.transform);
            chara.AddComponent<CharacterStats>();
            CharacterStats ch = chara.GetComponent<CharacterStats>();
            ch.newCharacterStats(this);
            ch.teamNo = t;
            chara.GetComponent<MeshRenderer>().material = mat;
            if (teamAi.strategyName == "JobBased")
            {
                assignJobBased(GameObject.Find("JobManager"), ch);
            }
        }
    }

    
        
    void assignJobBased(GameObject jobCon, CharacterStats cha)
    {
        w = jobCon.GetComponent<Woodcutter>();
        s = jobCon.GetComponent<Stonecutter>();
        a = jobCon.GetComponent<AnimalTamer>();
        c = jobCon.GetComponent<Crafter>();

        cha.currentJob = j;
    }

    void Update()
    {
        GameObject.Find("WoodRes1").GetComponent<Text>().text = ("" + GameObject.Find("Team1Spawn").GetComponent<Stockpile>().wood);
        GameObject.Find("StoneRes1").GetComponent<Text>().text = ("" + GameObject.Find("Team1Spawn").GetComponent<Stockpile>().stone);
        GameObject.Find("FoodRes1").GetComponent<Text>().text = ("" + GameObject.Find("Team1Spawn").GetComponent<Stockpile>().food);
        GameObject.Find("LeatherRes1").GetComponent<Text>().text = ("" + GameObject.Find("Team1Spawn").GetComponent<Stockpile>().leather);
        GameObject.Find("WoodRes2").GetComponent<Text>().text = ("" + GameObject.Find("Team2Spawn").GetComponent<Stockpile>().wood);
        GameObject.Find("StoneRes2").GetComponent<Text>().text = ("" + GameObject.Find("Team2Spawn").GetComponent<Stockpile>().stone);
        GameObject.Find("FoodRes2").GetComponent<Text>().text = ("" + GameObject.Find("Team2Spawn").GetComponent<Stockpile>().food);
        GameObject.Find("LeatherRes2").GetComponent<Text>().text = ("" + GameObject.Find("Team2Spawn").GetComponent<Stockpile>().leather);
    }



}
