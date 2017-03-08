using UnityEngine;
using System.Collections;
using System;

public class WorkingState : MonoBehaviour, IState
{

    GameObject character;
    BaseCharacter baseChar;

    public void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    public void ToAlertState()
    {
        throw new NotImplementedException();
    }

    public void ToChaseState()
    {
        throw new NotImplementedException();
    }

    public void ToFleeState()
    {
        throw new NotImplementedException();
    }

    public void ToHealState()
    {
        throw new NotImplementedException();
    }

    public void ToIdleState()
    {
        gameObject.GetComponent<BaseCharacter>().currentState = gameObject.GetComponent<BaseCharacter>().idleState;
    }

    public void ToPatrolState()
    {
        throw new NotImplementedException();
    }

    public void ToWorkingState()
    {
        throw new NotImplementedException();
    }

    public void UpdateState()
    {
        gameObject.GetComponent<BaseCharacter>().cutWood();
    }

    // Use this for initialization
    void Start () {

        character = this.gameObject;
        baseChar = gameObject.GetComponent<BaseCharacter>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
