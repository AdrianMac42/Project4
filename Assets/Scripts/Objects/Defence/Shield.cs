using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item {

    int AC = 2;
    void Awake()
    {
        woodReq = 10;
        stoneReq = 10;
        leatherReq = 2;
    }

}
