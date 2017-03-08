using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item{

    int maxDamage;
    int range;
    int toHitModifier;
    int quality;

    public int MaxDamage
    {
        get
        {
            return maxDamage;
        }

        set
        {
            maxDamage = value;
        }
    }

    public int Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    public int ToHitModifier
    {
        get
        {
            return toHitModifier;
        }

        set
        {
            toHitModifier = value;
        }
    }

    public int Quality
    {
        get
        {
            return quality;
        }

        set
        {
            quality = value;
        }
    }
}
