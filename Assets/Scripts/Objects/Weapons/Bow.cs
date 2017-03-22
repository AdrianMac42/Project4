using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{

    public int ammo = 20;
    
    public int woodReq = 20;
    public int stoneReq = 20;
    public int leatherReq = 5;

    void Awake()
    {
        range = 20;
        maxDamage = 6;

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
        if (ammo > 0)
        {
            int roll = Random.Range(1, 20);
            toHitModifier = attacker.dexterity;
            int toAttack = roll + toHitModifier;
            if (toAttack >= defender.armorClass)
            {
                int rollDmg = Random.Range(1, maxDamage) + attacker.dexterity + quality;
                hit(defender, rollDmg);
            }
            else
            {
                miss();
            }
            ammo--;
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
