using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unarmed : Weapon
{

    void Awake()
    {
        Range = 5;
        MaxDamage = 2;
    }

    public void attack(CharacterStats attacker, CharacterStats defender)
    {
        
        int roll = Random.Range(1, 20);
        ToHitModifier = attacker.strength;
        int toAttack = roll + ToHitModifier;
        if (toAttack >= defender.armorClass)
        {
            int rollDmg = Random.Range(1, MaxDamage) + attacker.strength;
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
