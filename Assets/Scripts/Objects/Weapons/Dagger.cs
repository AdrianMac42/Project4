using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{

    public int woodReq = 1;
    public int stoneReq = 1;
    public int leatherReq = 1;

    void Awake()
    {
        range = 1;
        maxDamage = 4;



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
        if (attacker.strength > attacker.dexterity)
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
        else
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
