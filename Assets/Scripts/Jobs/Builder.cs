﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Job {

    GameObject[] objs;
    CharacterStats character;
    void Start ()
    {
        Title = "Builder";
        NumberOfWorkers = 0;
        Priority = 0;
        TotalJobProduction = 0;
    }

    void Update()
    {
        objs = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject element in objs)
        {
            character = element.GetComponent<CharacterStats>();
            if (character.currentJob.Title == Title)
            {
                work(character);
            }
        }
    }

    void work(CharacterStats ch)
    {

    }





}
