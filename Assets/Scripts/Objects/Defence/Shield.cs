using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Armor {
    
    public int woodReq = 10;
    public int stoneReq = 10;
    public int leatherReq = 2;

    void update()
    {
        ac = 2 + quality;
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
