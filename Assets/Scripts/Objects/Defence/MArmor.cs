using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MArmor : Armor
{

    public int woodReq = 0;
    public int stoneReq = 0;
    public int leatherReq = 25;
    void update()
    {
        
        ac = 10 + wielder.dexterity;
        if (ac > 14)
        {
            ac = 14;
        }
        ac += quality;
    }

    public bool checkReq(GameObject x)
    {
        if (leatherReq <= x.GetComponent<Stockpile>().leather && stoneReq <= x.GetComponent<Stockpile>().stone && woodReq <= x.GetComponent<Stockpile>().wood)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
