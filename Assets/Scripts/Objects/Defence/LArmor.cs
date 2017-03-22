using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LArmor : Armor {


    public int woodReq = 0;
    public int stoneReq = 0;
    public int leatherReq = 10;
    void update ()
    {
        ac = 8 + wielder.dexterity + quality;
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
