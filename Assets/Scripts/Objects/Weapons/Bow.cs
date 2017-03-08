using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{

    int ammo = 20;

    void Awake()
    {
        Range = 40;
        MaxDamage = 6;
        woodReq = 20;
        stoneReq = 20;
        leatherReq = 5;

    }

    public void attack(CharacterStats attacker, CharacterStats defender)
    {
        if (ammo > 0)
        {
            int roll = Random.Range(1, 20);
            ToHitModifier = attacker.dexterity;
            int toAttack = roll + ToHitModifier;
            if (toAttack >= defender.armorClass)
            {
                int rollDmg = Random.Range(1, MaxDamage) + attacker.dexterity + Quality;
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
