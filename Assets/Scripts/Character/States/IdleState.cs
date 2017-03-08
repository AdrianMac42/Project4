using UnityEngine;
using System.Collections;
using System;




public class IdleState : MonoBehaviour , IState 
{
    GameObject character;
    BaseCharacter charVar;
    private IEnumerator coroutine;
    Vector3 target;
    Vector3 newTarget;

    void strole()
    {
        int ran = UnityEngine.Random.Range(1, 60);
        if (ran == 1)
        {
            target = new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0) ;
            newTarget = this.transform.position + target;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    public void ToAlertState()
    {
        gameObject.GetComponent<BaseCharacter>().currentState = gameObject.GetComponent<BaseCharacter>().alertState;
    }

    public void ToChaseState()
    {
        gameObject.GetComponent<BaseCharacter>().currentState = gameObject.GetComponent<BaseCharacter>().chaseState;
    }

    public void ToFleeState()
    {
        gameObject.GetComponent<BaseCharacter>().currentState = gameObject.GetComponent<BaseCharacter>().fleeState;
    }

    public void ToHealState()
    {
        gameObject.GetComponent<BaseCharacter>().currentState = gameObject.GetComponent<BaseCharacter>().healState;
    }

    public void ToIdleState()
    {
        // Already in this state
    }

    public void ToPatrolState()
    {
        gameObject.GetComponent<BaseCharacter>().currentState = gameObject.GetComponent<BaseCharacter>().patrolState;
    }

    public void ToWorkingState()
    {
        gameObject.GetComponent<BaseCharacter>().currentState = gameObject.GetComponent<BaseCharacter>().workingState;
    }


    public void checkForWork()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Tree");
        if (targets.Length >= 1)
        {
            foreach (GameObject tree in targets)
            {
                if ((Vector3.Distance(tree.transform.position, this.transform.position)) <= gameObject.GetComponent<BaseCharacter>().viewRange)
                {
                    ToWorkingState();
                }
            }
        }
    }



    public void UpdateState()
    {
        strole();

        if (Vector3.Distance(this.transform.position, newTarget) >= 0.5f)
        {
            this.transform.position += target * gameObject.GetComponent<BaseCharacter>().speed * Time.deltaTime;
        }
        checkForWork();
    }

    // Use this for initialization
    void Start () {
        target = new Vector3(0,0,0);
        newTarget = this.transform.position;
        character = this.gameObject;
        charVar = gameObject.GetComponent<BaseCharacter>();
    }

}



