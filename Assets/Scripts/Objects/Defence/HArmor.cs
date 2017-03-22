using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HArmor : Armor {

    public int woodReq = 0;
    public int stoneReq = 0;
    public int leatherReq = 50;
    void update()
    {
        
        
        
        ac = 16 + quality;
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
