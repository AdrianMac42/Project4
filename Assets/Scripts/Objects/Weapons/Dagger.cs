using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{

    void Awake()
    {
        Range = 5;
        MaxDamage = 4;
        woodReq = 1;
        stoneReq = 1;
        leatherReq = 1;
    }

    public void attack(CharacterStats attacker, CharacterStats defender)
    {
        if (attacker.strength > attacker.dexterity)
        {
            int roll = Random.Range(1, 20);
            ToHitModifier = attacker.strength;
            int toAttack = roll + ToHitModifier;
            if (toAttack >= defender.armorClass)
            {
                int rollDmg = Random.Range(1, MaxDamage) + attacker.strength + Quality;
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
            ToHitModifier = attacker.dexterity;
            int toAttack = roll + ToHitModifier;
            if (toAttack >= defender.armorClass)
            {
                int rollDmg = Random.Range(1, MaxDamage) + attacker.dexterity;
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
