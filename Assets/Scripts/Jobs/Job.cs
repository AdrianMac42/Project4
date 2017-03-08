using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : MonoBehaviour{

    public string title;
    public int numberOfWorkers;
    public int priority;
    public int totalJobProduction;

    public string Title
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }

    public int NumberOfWorkers
    {
        get
        {
            return numberOfWorkers;
        }

        set
        {
            numberOfWorkers = value;
        }
    }

    public int Priority
    {
        get
        {
            return priority;
        }

        set
        {
            priority = value;
        }
    }

    public int TotalJobProduction
    {
        get
        {
            return totalJobProduction;
        }

        set
        {
            totalJobProduction = value;
        }
    }
}
