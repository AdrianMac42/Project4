using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : Job {

    GameObject[] objs;
    int noInTeam;

    GameObject[] armories;
    GameObject armory;
    bool armoryFull = false;
    Sword sword;
    Dagger dagger;
    Bow bow; 
    Shield shield;
    LArmor larm;
    MArmor marm;
    HArmor harm;

    void Start ()
    {
        title = "Crafter";
        numberOfWorkers1 = 0;
        numberOfWorkers2 = 0;
        priority = 0;
        totalJobProduction = 0;
        sword = new Sword();
        dagger = new Dagger();
        bow = new Bow();
        shield = new Shield();
        larm = new LArmor();
        marm = new MArmor();
        harm = new HArmor();
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

                if (GameObject.Find("GameController").GetComponent<PhaseManager>().phaseNo <= 1)
                {
                    work(element.GetComponent<CharacterStats>());
                }
                else
                {
                    finishWork(element.GetComponent<CharacterStats>());
                }
            }
        }
    }


    void finishWork(CharacterStats ch)
    {
        if (ch.currentCarryLoad > 0)
        {
            if (ch.teamNo == 1)
            {
                ch.targetStockpile = GameObject.Find("Team1Spawn");
            }
            else if (ch.teamNo == 2)
            {
                ch.targetStockpile = GameObject.Find("Team2Spawn");
            }
            else
            {
                Debug.LogError("STOCKPILE NOT ASSIGNED");
            }
            // if not withing range...move to it
            if (Vector3.Distance(ch.transform.position, ch.targetStockpile.transform.position) >= 0.5f)
            {
                ch.transform.position = Vector3.MoveTowards(ch.transform.position, ch.targetStockpile.transform.position, (ch.speed * Time.smoothDeltaTime));
            }
            else // if in range...deposit
            {
                ch.targetStockpile.GetComponent<Stockpile>().stone += ch.currentCarryLoad;
                ch.currentCarryLoad -= ch.currentCarryLoad;
            }
        }
        else
        {
            ch.armUp();
        }
    }

    void craftSword(CharacterStats character)
    {
        if (sword.checkReq(character.targetStockpile) && character.collected != true && character.crafted != true)//Change
        {
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if(character.teamNo == 1)
            {
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractLeather(sword.leatherReq);// Change
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractStone(sword.stoneReq);
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractWood(sword.woodReq);
                character.collected = true;
            }
            else if (character.teamNo == 2)
            {
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractLeather(sword.leatherReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractStone(sword.stoneReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractWood(sword.woodReq);
                character.collected = true;
            }
        }
        else if (character.collected == true && character.crafted == false)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (character.currentWorkTarget.tag == "Workshop" && Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Workshop")
            {
                sword.quality = character.crafting;// Change
                character.equipRight(sword);// Change
                character.crafted = true;
                character.collected = false;
            }
            else
            {
                Debug.LogError("Cant find Workshop");
            }
        }
        if (character.crafted == true)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if(character.currentWorkTarget.tag == "Armory")
            {
                character.currentWorkTarget.GetComponent<Armory>().swords.Add(character.rHand);// Change
                character.rHand = null;
                character.collected = false;
                character.crafted = false;
            }
        }
    }

    void craftDagger(CharacterStats character)
    {
        if (dagger.checkReq(character.targetStockpile) && character.collected != true && character.crafted != true)//Change
        {
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.teamNo == 1)
            {
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractLeather(dagger.leatherReq);// Change
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractStone(dagger.stoneReq);
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractWood(dagger.woodReq);
                character.collected = true;
            }
            else if (character.teamNo == 2)
            {
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractLeather(dagger.leatherReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractStone(dagger.stoneReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractWood(dagger.woodReq);
                character.collected = true;
            }
        }
        else if (character.collected == true && character.crafted == false)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (character.currentWorkTarget.tag == "Workshop" && Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Workshop")
            {
                dagger.quality = character.crafting;// Change
                character.equipRight(dagger);// Change
                character.crafted = true;
                character.collected = false;
            }
            else
            {
                Debug.LogError("Cant find Workshop");
            }
        }
        if (character.crafted == true)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Armory")
            {
                character.currentWorkTarget.GetComponent<Armory>().daggers.Add(character.rHand);// Change
                character.rHand = null;
                character.collected = false;
                character.crafted = false;
            }
        }
    }

    void craftBow(CharacterStats character)
    {
        if (bow.checkReq(character.targetStockpile) && character.collected != true && character.crafted != true)//Change
        {
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.teamNo == 1)
            {
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractLeather(bow.leatherReq);// Change
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractStone(bow.stoneReq);
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractWood(bow.woodReq);
                character.collected = true;
            }
            else if (character.teamNo == 2)
            {
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractLeather(bow.leatherReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractStone(bow.stoneReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractWood(bow.woodReq);
                character.collected = true;
            }
        }
        else if (character.collected == true && character.crafted == false)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (character.currentWorkTarget.tag == "Workshop" && Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Workshop")
            {
                bow.quality = character.crafting;// Change
                character.equipRight(bow);// Change
                character.crafted = true;
                character.collected = false;
            }
            else
            {
                Debug.LogError("Cant find Workshop");
            }
        }
        if (character.crafted == true)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Armory")
            {
                character.currentWorkTarget.GetComponent<Armory>().bows.Add(character.rHand);// Change
                character.rHand = null;
                character.collected = false;
                character.crafted = false;
            }
        }
    }

    void craftHArmor(CharacterStats character)
    {
        if (harm.checkReq(character.targetStockpile) && character.collected != true && character.crafted != true)//Change
        {
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.teamNo == 1)
            {
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractLeather(harm.leatherReq);// Change
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractStone(harm.stoneReq);
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractWood(harm.woodReq);
                character.collected = true;
            }
            else if (character.teamNo == 2)
            {
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractLeather(harm.leatherReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractStone(harm.stoneReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractWood(harm.woodReq);
                character.collected = true;
            }
        }
        else if (character.collected == true && character.crafted == false)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (character.currentWorkTarget.tag == "Workshop" && Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Workshop")
            {
                harm.quality = character.crafting;// Change
                character.equipRight(harm);// Change
                character.crafted = true;
                character.collected = false;
            }
            else
            {
                Debug.LogError("Cant find Workshop");
            }
        }
        if (character.crafted == true)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Armory")
            {
                character.currentWorkTarget.GetComponent<Armory>().harmor.Add(character.rHand);// Change
                character.rHand = null;
                character.collected = false;
                character.crafted = false;
            }
        }
    }

    void craftMArmor(CharacterStats character)
    {
        if (marm.checkReq(character.targetStockpile) && character.collected != true && character.crafted != true)//Change
        {
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.teamNo == 1)
            {
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractLeather(marm.leatherReq);// Change
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractStone(marm.stoneReq);
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractWood(marm.woodReq);
                character.collected = true;
            }
            else if (character.teamNo == 2)
            {
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractLeather(marm.leatherReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractStone(marm.stoneReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractWood(marm.woodReq);
                character.collected = true;
            }
        }
        else if (character.collected == true && character.crafted == false)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (character.currentWorkTarget.tag == "Workshop" && Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Workshop")
            {
                marm.quality = character.crafting;// Change
                character.equipRight(marm);// Change
                character.crafted = true;
                character.collected = false;
            }
            else
            {
                Debug.LogError("Cant find Workshop");
            }
        }
        if (character.crafted == true)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Armory")
            {
                character.currentWorkTarget.GetComponent<Armory>().marmor.Add(character.rHand);// Change
                character.rHand = null;
                character.collected = false;
                character.crafted = false;
            }
        }
    }

    void craftLArmor(CharacterStats character)
    {
        if (larm.checkReq(character.targetStockpile) && character.collected != true && character.crafted != true)//Change
        {
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.teamNo == 1)
            {
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractLeather(larm.leatherReq);// Change
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractStone(larm.stoneReq);
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractWood(larm.woodReq);
                character.collected = true;
            }
            else if (character.teamNo == 2)
            {
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractLeather(larm.leatherReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractStone(larm.stoneReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractWood(larm.woodReq);
                character.collected = true;
            }
        }
        else if (character.collected == true && character.crafted == false)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (character.currentWorkTarget.tag == "Workshop" && Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Workshop")
            {
                larm.quality = character.crafting;// Change
                character.equipRight(larm);// Change
                character.crafted = true;
                character.collected = false;
            }
            else
            {
                Debug.LogError("Cant find Workshop");
            }
        }
        if (character.crafted == true)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Armory")
            {
                character.currentWorkTarget.GetComponent<Armory>().larmor.Add(character.rHand);// Change
                character.rHand = null;
                character.collected = false;
                character.crafted = false;
            }
        }
    }

    void craftShield(CharacterStats character)
    {
        if (shield.checkReq(character.targetStockpile) && character.collected != true && character.crafted != true)//Change
        {
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.teamNo == 1)
            {
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractLeather(shield.leatherReq);// Change
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractStone(shield.stoneReq);
                GameObject.Find("Team1Spawn").GetComponent<Stockpile>().subtractWood(shield.woodReq);
                character.collected = true;
            }
            else if (character.teamNo == 2)
            {
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractLeather(shield.leatherReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractStone(shield.stoneReq);
                GameObject.Find("Team2Spawn").GetComponent<Stockpile>().subtractWood(shield.woodReq);
                character.collected = true;
            }
        }
        else if (character.collected == true && character.crafted == false)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (character.currentWorkTarget.tag == "Workshop" && Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Workshop")
            {
                shield.quality = character.crafting;// Change
                character.equipRight(shield);// Change
                character.crafted = true;
                character.collected = false;
            }
            else
            {
                Debug.LogError("Cant find Workshop");
            }
        }
        if (character.crafted == true)
        {
            character.currentWorkTarget = null;
            character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in character.workTargets)
            {
                if (character.currentWorkTarget == null)
                {
                    character.currentWorkTarget = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position))
                    < (Vector3.Distance(character.currentWorkTarget.transform.position, character.targetStockpile.GetComponent<Stockpile>().transform.position)))
                {
                    character.currentWorkTarget = tar;
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else if (character.currentWorkTarget.tag == "Armory")
            {
                character.currentWorkTarget.GetComponent<Armory>().shields.Add(character.rHand);// Change
                character.rHand = null;
                character.collected = false;
                character.crafted = false;
            }
        }
    }

    void work(CharacterStats ch)
    {
        noInTeam = ch.team.teamCount;
        if (ch.teamNo == 1)
        {
            ch.targetStockpile = GameObject.Find("Team1Spawn");
        }
        else if (ch.teamNo == 2)
        {
            ch.targetStockpile = GameObject.Find("Team2Spawn");
        }
        else
        {
            Debug.LogError("STOCKPILE NOT ASSIGNED");
        }
        ch.currentWorkTarget = null;
        ch.workTargets = GameObject.FindGameObjectsWithTag("Workshop");
        foreach (GameObject tar in ch.workTargets)
        {
            if (ch.currentWorkTarget == null)
            {
                ch.currentWorkTarget = tar;
            }
            else if ((Vector3.Distance(tar.transform.position, ch.transform.position))
                < (Vector3.Distance(ch.currentWorkTarget.transform.position, ch.transform.position)))
            {
                ch.currentWorkTarget = tar;
            }
        }
        if (ch.workTargets.Length >= 1)
        {
            //craft...
            armory = null;
            armories = GameObject.FindGameObjectsWithTag("Armory");
            foreach (GameObject tar in armories)
            {
                if (armory == null)
                {
                    armory = tar;
                }
                else if ((Vector3.Distance(tar.transform.position, ch.targetStockpile.transform.position))
                    < (Vector3.Distance(armory.transform.position, ch.targetStockpile.transform.position)))
                {
                    armory = tar;
                }
            }
            // Craft in priority....
            if (sword.checkReq(ch.targetStockpile) && armory.GetComponent<Armory>().swords.Count < ch.team.teamCount)
            {
                craftSword(ch);
            }
            else if (harm.checkReq(ch.targetStockpile) && armory.GetComponent<Armory>().harmor.Count < ch.team.teamCount)
            {
                craftHArmor(ch);
            }
            else if (bow.checkReq(ch.targetStockpile) && armory.GetComponent<Armory>().bows.Count < ch.team.teamCount)
            {
                craftBow(ch);
            }
            else if (marm.checkReq(ch.targetStockpile) && armory.GetComponent<Armory>().marmor.Count < ch.team.teamCount)
            {
                craftMArmor(ch);
            }
            else if (shield.checkReq(ch.targetStockpile) && armory.GetComponent<Armory>().shields.Count < ch.team.teamCount)
            {
                craftShield(ch);
            }
            else if (larm.checkReq(ch.targetStockpile) && armory.GetComponent<Armory>().larmor.Count < ch.team.teamCount)
            {
                craftLArmor(ch);
            }
            else if (dagger.checkReq(ch.targetStockpile) && armory.GetComponent<Armory>().daggers.Count < ch.team.teamCount)
            {
                craftDagger(ch);
            }
            else
            {
                ch.currentWorkTarget = null;
                strole(ch);
            }
        }
        else
        {
            ch.currentWorkTarget = null;
            strole(ch);
        }
    }
    
    void strole(CharacterStats c)
    {
        if (c.strolePos == null)
        {
            c.strolePos = c.transform.position;
        }
        int ran = UnityEngine.Random.Range(1, 60);
        if (ran == 1)
        {
            c.strolePos += new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0);
        }

        c.transform.position = Vector3.MoveTowards(c.transform.position, c.strolePos, (c.speed * Time.smoothDeltaTime));
    }
}
