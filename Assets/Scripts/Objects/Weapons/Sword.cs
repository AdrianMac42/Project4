using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    public int woodReq    = 4;
    public int stoneReq   = 10;
    public int leatherReq = 2;

    public void awake()
    {
        range = 1;
        maxDamage = 8;
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
    

    public void attack(CharacterStats attacker, CharacterStats defender)
    {
        int roll = Random.Range(1, 20);
        toHitModifier = attacker.strength;
        int toAttack = roll + toHitModifier;
        if (toAttack >= defender.armorClass)
        {
            int rollDmg = Random.Range(1, maxDamage) + attacker.strength + quality;
            hit(defender, rollDmg);
        }
        else
        {
            miss();
        }

    }

    public void hit(CharacterStats target, int damage)
    {
        target.takeDamage(damage);
        //Display damage dealt
    }

    public void miss()
    {
        //Display miss text
    }


}
