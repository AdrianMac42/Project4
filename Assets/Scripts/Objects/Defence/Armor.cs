using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item {

    CharacterStats wielder;
    int ac;

    public int Ac
    {
        get
        {
            return ac;
        }

        set
        {
            ac = value;
        }
    }

    public CharacterStats Wielder
    {
        get
        {
            return wielder;
        }

        set
        {
            wielder = value;
        }
    }
}
