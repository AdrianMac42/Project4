using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : Job {

    GameObject[] objs;
    CharacterStats character;
    int noInTeam;
    GameObject[] armories;
    GameObject armory;
    bool armoryFull = false;
    Sword sword = new Sword();
    Dagger dagger = new Dagger();
    Bow bow = new Bow();
    Shield shield = new Shield();
    LArmor larm = new LArmor();
    MArmor marm = new MArmor();
    HArmor harm = new HArmor();

    void Start ()
    {
        Title = "Crafter";
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

    void craftSword(CharacterStats character, Stockpile stock)
    {
        if (checkReq(character.targetStockpile.GetComponent<Stockpile>(), sword) && character.collected != true)
        {
            Item iCraft = sword;
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                stock.leather -= iCraft.leatherReq;
                stock.stone -= iCraft.stoneReq;
                stock.wood -= iCraft.woodReq;
                character.collected = true;
            }
        }
        else if (character.collected == true && character.currentCarryLoad < character.maxCarryLoad)
        {
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                character.currentCarryLoad = character.maxCarryLoad;
                Sword weapon = new Sword();
                weapon.Quality = character.crafting;
                character.equipRight(weapon);
            }
        }
        else if (character.currentCarryLoad == character.maxCarryLoad)
        {
            if (character.currentWorkTarget.tag != "Armory")
            {
                character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
                foreach (GameObject tar in character.workTargets)
                {
                    if (character.currentWorkTarget == null)
                    {
                        character.currentWorkTarget = tar;
                    }
                    else if ((Vector3.Distance(tar.transform.position, stock.transform.position))
                        < (Vector3.Distance(character.currentWorkTarget.transform.position, stock.transform.position)))
                    {
                        character.currentWorkTarget = tar;
                    }
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {

                character.currentCarryLoad = 0;
                character.currentWorkTarget.GetComponent<Armory>().swords.Add(character.rHand);
                character.rHand = null;
                character.collected = false;
            }
        }
    }

    void craftDagger(CharacterStats character, Stockpile stock)
    {
        if (checkReq(character.targetStockpile.GetComponent<Stockpile>(), dagger) && character.collected != true)
        {
            Item iCraft = dagger;
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                stock.leather -= iCraft.leatherReq;
                stock.stone -= iCraft.stoneReq;
                stock.wood -= iCraft.woodReq;
                character.collected = true;
            }
        }
        else if (character.collected == true && character.currentCarryLoad < character.maxCarryLoad)
        {
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                character.currentCarryLoad = character.maxCarryLoad;
                Dagger weapon = new Dagger();
                weapon.Quality = character.crafting;
                character.equipRight(weapon);
            }
        }
        else if (character.currentCarryLoad == character.maxCarryLoad)
        {
            if (character.currentWorkTarget.tag != "Armory")
            {
                character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
                foreach (GameObject tar in character.workTargets)
                {
                    if (character.currentWorkTarget == null)
                    {
                        character.currentWorkTarget = tar;
                    }
                    else if ((Vector3.Distance(tar.transform.position, stock.transform.position))
                        < (Vector3.Distance(character.currentWorkTarget.transform.position, stock.transform.position)))
                    {
                        character.currentWorkTarget = tar;
                    }
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {

                character.currentCarryLoad = 0;
                character.currentWorkTarget.GetComponent<Armory>().daggers.Add(character.rHand);
                character.rHand = null;
                character.collected = false;
            }
        }
    }

    void craftBow(CharacterStats character, Stockpile stock)
    {
        if (checkReq(character.targetStockpile.GetComponent<Stockpile>(), bow) && character.collected != true)
        {
            Item iCraft = bow;
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                stock.leather -= iCraft.leatherReq;
                stock.stone -= iCraft.stoneReq;
                stock.wood -= iCraft.woodReq;
                character.collected = true;
            }
        }
        else if (character.collected == true && character.currentCarryLoad < character.maxCarryLoad)
        {
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                character.currentCarryLoad = character.maxCarryLoad;
                Bow weapon = new Bow();
                weapon.Quality = character.crafting;
                character.equipRight(weapon);
            }
        }
        else if (character.currentCarryLoad == character.maxCarryLoad)
        {
            if (character.currentWorkTarget.tag != "Armory")
            {
                character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
                foreach (GameObject tar in character.workTargets)
                {
                    if (character.currentWorkTarget == null)
                    {
                        character.currentWorkTarget = tar;
                    }
                    else if ((Vector3.Distance(tar.transform.position, stock.transform.position))
                        < (Vector3.Distance(character.currentWorkTarget.transform.position, stock.transform.position)))
                    {
                        character.currentWorkTarget = tar;
                    }
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {

                character.currentCarryLoad = 0;
                character.currentWorkTarget.GetComponent<Armory>().bows.Add(character.rHand);
                character.rHand = null;
                character.collected = false;
            }
        }
    }

    void craftHArmor(CharacterStats character, Stockpile stock)
    {
        if (checkReq(character.targetStockpile.GetComponent<Stockpile>(), harm) && character.collected != true)
        {
            Item iCraft = harm;
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                stock.leather -= iCraft.leatherReq;
                stock.stone -= iCraft.stoneReq;
                stock.wood -= iCraft.woodReq;
                character.collected = true;
            }
        }
        else if (character.collected == true && character.currentCarryLoad < character.maxCarryLoad)
        {
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                character.currentCarryLoad = character.maxCarryLoad;
                HArmor weapon = new HArmor();
                character.equipRight(weapon);
            }
        }
        else if (character.currentCarryLoad == character.maxCarryLoad)
        {
            if (character.currentWorkTarget.tag != "Armory")
            {
                character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
                foreach (GameObject tar in character.workTargets)
                {
                    if (character.currentWorkTarget == null)
                    {
                        character.currentWorkTarget = tar;
                    }
                    else if ((Vector3.Distance(tar.transform.position, stock.transform.position))
                        < (Vector3.Distance(character.currentWorkTarget.transform.position, stock.transform.position)))
                    {
                        character.currentWorkTarget = tar;
                    }
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {

                character.currentCarryLoad = 0;
                character.currentWorkTarget.GetComponent<Armory>().harmor.Add(character.rHand);
                character.rHand = null;
                character.collected = false;
            }
        }
    }

    void craftMArmor(CharacterStats character, Stockpile stock)
    {
        if (checkReq(character.targetStockpile.GetComponent<Stockpile>(), marm) && character.collected != true)
        {
            Item iCraft = marm;
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                stock.leather -= iCraft.leatherReq;
                stock.stone -= iCraft.stoneReq;
                stock.wood -= iCraft.woodReq;
                character.collected = true;
            }
        }
        else if (character.collected == true && character.currentCarryLoad < character.maxCarryLoad)
        {
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                character.currentCarryLoad = character.maxCarryLoad;
                MArmor weapon = new MArmor();
                character.equipRight(weapon);
            }
        }
        else if (character.currentCarryLoad == character.maxCarryLoad)
        {
            if (character.currentWorkTarget.tag != "Armory")
            {
                character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
                foreach (GameObject tar in character.workTargets)
                {
                    if (character.currentWorkTarget == null)
                    {
                        character.currentWorkTarget = tar;
                    }
                    else if ((Vector3.Distance(tar.transform.position, stock.transform.position))
                        < (Vector3.Distance(character.currentWorkTarget.transform.position, stock.transform.position)))
                    {
                        character.currentWorkTarget = tar;
                    }
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {

                character.currentCarryLoad = 0;
                character.currentWorkTarget.GetComponent<Armory>().marmor.Add(character.rHand);
                character.rHand = null;
                character.collected = false;
            }
        }
    }

    void craftLArmor(CharacterStats character, Stockpile stock)
    {
        if (checkReq(character.targetStockpile.GetComponent<Stockpile>(), larm) && character.collected != true)
        {
            Item iCraft = larm;
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                stock.leather -= iCraft.leatherReq;
                stock.stone -= iCraft.stoneReq;
                stock.wood -= iCraft.woodReq;
                character.collected = true;
            }
        }
        else if (character.collected == true && character.currentCarryLoad < character.maxCarryLoad)
        {
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                character.currentCarryLoad = character.maxCarryLoad;
                LArmor weapon = new LArmor();
                character.equipRight(weapon);
            }
        }
        else if (character.currentCarryLoad == character.maxCarryLoad)
        {
            if (character.currentWorkTarget.tag != "Armory")
            {
                character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
                foreach (GameObject tar in character.workTargets)
                {
                    if (character.currentWorkTarget == null)
                    {
                        character.currentWorkTarget = tar;
                    }
                    else if ((Vector3.Distance(tar.transform.position, stock.transform.position))
                        < (Vector3.Distance(character.currentWorkTarget.transform.position, stock.transform.position)))
                    {
                        character.currentWorkTarget = tar;
                    }
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {

                character.currentCarryLoad = 0;
                character.currentWorkTarget.GetComponent<Armory>().larmor.Add(character.rHand);
                character.rHand = null;
                character.collected = false;
            }
        }
    }

    void craftShield(CharacterStats character, Stockpile stock)
    {
        if (checkReq(character.targetStockpile.GetComponent<Stockpile>(), shield) && character.collected != true)
        {
            Item iCraft = shield;
            if (Vector3.Distance(character.transform.position, character.targetStockpile.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetStockpile.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                stock.leather -= iCraft.leatherReq;
                stock.stone -= iCraft.stoneReq;
                stock.wood -= iCraft.woodReq;
                character.collected = true;
            }
        }
        else if (character.collected == true && character.currentCarryLoad < character.maxCarryLoad)
        {
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {
                character.currentCarryLoad = character.maxCarryLoad;
                Shield weapon = new Shield();
                character.equipRight(weapon);
            }
        }
        else if (character.currentCarryLoad == character.maxCarryLoad)
        {
            if (character.currentWorkTarget.tag != "Armory")
            {
                character.workTargets = GameObject.FindGameObjectsWithTag("Armory");
                foreach (GameObject tar in character.workTargets)
                {
                    if (character.currentWorkTarget == null)
                    {
                        character.currentWorkTarget = tar;
                    }
                    else if ((Vector3.Distance(tar.transform.position, stock.transform.position))
                        < (Vector3.Distance(character.currentWorkTarget.transform.position, stock.transform.position)))
                    {
                        character.currentWorkTarget = tar;
                    }
                }
            }
            if (Vector3.Distance(character.transform.position, character.currentWorkTarget.transform.position) >= 0.5f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.currentWorkTarget.transform.position, (character.speed * Time.smoothDeltaTime));
            }
            else
            {

                character.currentCarryLoad = 0;
                character.currentWorkTarget.GetComponent<Armory>().shields.Add(character.rHand);
                character.rHand = null;
                character.collected = false;
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
        if (ch.currentCarryLoad < ch.maxCarryLoad && ch.workTargets.Length >= 1 && armoryFull == false)
        {
            //craft...
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
            if (checkReq(ch.targetStockpile.GetComponent<Stockpile>(), sword) && armory.GetComponent<Armory>().swords.Count < ch.team.teamCount)
            {
                craftSword(ch, ch.targetStockpile.GetComponent<Stockpile>());
            }
            else if (checkReq(ch.targetStockpile.GetComponent<Stockpile>(), harm) && armory.GetComponent<Armory>().harmor.Count < ch.team.teamCount)
            {
                craftHArmor(ch, ch.targetStockpile.GetComponent<Stockpile>());
            }
            else if (checkReq(ch.targetStockpile.GetComponent<Stockpile>(), bow) && armory.GetComponent<Armory>().bows.Count < ch.team.teamCount)
            {
                craftBow(ch, ch.targetStockpile.GetComponent<Stockpile>());
            }
            else if (checkReq(ch.targetStockpile.GetComponent<Stockpile>(), marm) && armory.GetComponent<Armory>().marmor.Count < ch.team.teamCount)
            {
                craftMArmor(ch, ch.targetStockpile.GetComponent<Stockpile>());
            }
            else if (checkReq(ch.targetStockpile.GetComponent<Stockpile>(), shield) && armory.GetComponent<Armory>().shields.Count < ch.team.teamCount)
            {
                craftShield(ch, ch.targetStockpile.GetComponent<Stockpile>());
            }
            else if (checkReq(ch.targetStockpile.GetComponent<Stockpile>(), larm) && armory.GetComponent<Armory>().larmor.Count < ch.team.teamCount)
            {
                craftLArmor(ch, ch.targetStockpile.GetComponent<Stockpile>());
            }
            else if (checkReq(ch.targetStockpile.GetComponent<Stockpile>(), dagger) && armory.GetComponent<Armory>().daggers.Count < ch.team.teamCount)
            {
                craftDagger(ch, ch.targetStockpile.GetComponent<Stockpile>());
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
    
    bool checkReq(Stockpile stock, Item i)
    {
        if (i.leatherReq >= stock.leather && i.stoneReq >= stock.stone && i.woodReq >= stock.wood )
        {
            return true;
        }
        else
        {
            return false;
        }
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
