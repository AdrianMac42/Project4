using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

    public string characterName;
    public int age;
    public float health = 100;
    public float morale;
    public string team;
    public float viewRange = 20;
    // States
    public IState currentState;
    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public FleeState fleeState;
    [HideInInspector]
    public HealState healState;
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public WorkingState workingState;

    // Skills
    public int strength = 0;
    public int accuracy = 0;
    public int aid = 0;
    public int leadership = 0;
    public int speed;
    public int maxCarryLoad;
    public int currentCarryLoad;
    public int woodCutting;

    public GameObject[] targets;
    public GameObject targetTree = null;
    public GameObject[] stockpiles;
    public GameObject targetStockpile = null;


    System.Random rnd = new System.Random();


    private void Awake()
    {
        chaseState = gameObject.AddComponent<ChaseState>();
        alertState = gameObject.AddComponent<AlertState>();
        patrolState = gameObject.AddComponent<PatrolState>();
        fleeState = gameObject.AddComponent<FleeState>();
        healState = gameObject.AddComponent<HealState>();
        idleState = gameObject.AddComponent<IdleState>();
        workingState = gameObject.AddComponent<WorkingState>();
    }

    // Use this for initialization
    void Start()
    {
        int[] stats = { strength, accuracy, aid, leadership, woodCutting };
        randomiseStats(stats);
        speed = 2;
        currentState = workingState;


        maxCarryLoad = strength;
        currentCarryLoad = 0;


    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();

    }


    void randomiseStats(int[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            int ran1 = rnd.Next(0, 6);
            int ran2 = rnd.Next(0, 6);
            int ran3 = rnd.Next(0, 6);
            int ran4 = rnd.Next(0, 2);
            int value = (ran1 + ran2 + ran3 + ran4);
            stats[i] += value;
        }
        strength = stats[0];
        accuracy = stats[1];
        aid = stats[2];
        leadership = stats[3];
        woodCutting = stats[4];
    }

    public void cutWood()
    {

        targets = GameObject.FindGameObjectsWithTag("Tree");
        if (currentCarryLoad < maxCarryLoad && targets.Length >= 1)
        {
            foreach (GameObject tree in targets)
            {
                if (targetTree == null)
                {
                    targetTree = tree;
                }
                else if ((Vector3.Distance(tree.transform.position, transform.position)) < (Vector3.Distance(targetTree.transform.position, transform.position)))
                {
                    targetTree = tree;
                }
                else if ((Vector3.Distance(targetTree.transform.position, this.transform.position)) > viewRange)
                {
                    workingState.ToIdleState();
                }
            }
            // if theres a target within range
            if (targetTree && (Vector3.Distance(targetTree.transform.position, this.transform.position)) <= viewRange) 
            {
                Vector3 targetLoc = (targetTree.transform.position - this.transform.position);
                // if not withing range...move to it
                if (Vector3.Distance(this.transform.position, targetTree.transform.position) >= 0.5f) 
                {
                    this.transform.position += targetLoc * speed * Time.deltaTime;
                }
                else // if in range...cut
                {
                    targetTree.GetComponent<Trees>().wood -= 1;
                    currentCarryLoad += 1;
                }   
            }
        }
        else if (currentCarryLoad > 0)
        {
            stockpiles = GameObject.FindGameObjectsWithTag("Stockpile");
            if (stockpiles.Length >= 1)
            {
                foreach (GameObject pile in stockpiles)
                {
                    if (targetStockpile == null)
                    {
                        targetStockpile = pile;
                    }
                    else if ((Vector3.Distance(pile.transform.position, transform.position)) < 
                        (Vector3.Distance(targetStockpile.transform.position, transform.position)))
                    {
                        targetStockpile = pile;
                    }

                }
            }
            Vector3 targetLoc = (targetStockpile.transform.position - this.transform.position);
            // if not withing range...move to it
            if (Vector3.Distance(this.transform.position, targetStockpile.transform.position) >= 0.5f) 
            {
                this.transform.position += targetLoc * speed * Time.deltaTime;
            }
            else // if in range...deposit
            {
                targetStockpile.GetComponent<Stockpile>().wood += currentCarryLoad;
                currentCarryLoad -= currentCarryLoad;
            }
            
        }
        else
        {
            currentState.ToIdleState();
        }
    }






    

    


}
