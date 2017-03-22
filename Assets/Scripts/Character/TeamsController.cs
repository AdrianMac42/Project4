using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamsController : MonoBehaviour {

    public Team team1;
    public Team team2;
    public string t1;
    public string t2;
    public GameObject teamPlaceholder;
    public GameObject character;
    public Material red;
    public Material blue;
    AiStrategy job;
    AiStrategy goal;

    public void spawnTeams()
    {
        job = new AiStrategy();
        goal = new AiStrategy();
        job.strategyName = "JobBased";
        goal.strategyName = "GoalBased";
        t1 = GameObject.Find("Team1Name").GetComponent<InputField>().text;
        t2 = GameObject.Find("Team2Name").GetComponent<InputField>().text;
        GameObject.Find("Team1NameNew").GetComponent<Text>().text = ("Team " + t1);
        GameObject.Find("Team2NameNew").GetComponent<Text>().text = ("Team " + t2);
        GameObject.Find("Team1Name").SetActive(false);
        GameObject.Find("Team2Name").SetActive(false);
        GameObject.Find("GenerateTeams").SetActive(false);
        team1 = Instantiate(teamPlaceholder.GetComponent<Team>(), gameObject.transform);
        team2 = Instantiate(teamPlaceholder.GetComponent<Team>(), gameObject.transform);
        team1.name = ("Team " + t1);
        team2.name = ("Team " + t2);
        team1.mat = red;
        team2.mat = blue;
        team1.makeTeam(character,t1,goal,1);
        team2.makeTeam(character,t2, job, 2);
        Debug.Log("Team 1 = " + team1.teamName);
        Debug.Log("Team 2 = " + team2.teamName);


    }
}
