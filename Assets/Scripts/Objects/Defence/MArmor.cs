using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MArmor : Armor
{


    void Awake()
    {
        woodReq = 0;
        stoneReq = 0;
        leatherReq = 25;
        Ac = 10 + Wielder.dexterity;
        if (Ac > 14)
        {
            Ac = 14;
        }
    }

}
