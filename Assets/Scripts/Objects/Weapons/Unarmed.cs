using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unarmed : Weapon
{

    void Awake()
    {
        range = 1;
        maxDamage = 2;
    }

    public void attack(CharacterStats attacker, CharacterStats defender)
    {
        
        int roll = Random.Range(1, 20);
        toHitModifier = attacker.strength;
        int toAttack = roll + toHitModifier;
        if (toAttack >= defender.armorClass)
        {
            int rollDmg = Random.Range(1, maxDamage) + attacker.strength;
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
