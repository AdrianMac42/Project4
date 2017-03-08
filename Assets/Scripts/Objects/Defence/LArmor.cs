using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LArmor : Armor {


    void Awake ()
    {
        woodReq = 0;
        stoneReq = 0;
        leatherReq = 10;
        Ac = 8 + Wielder.dexterity;
    }

}
