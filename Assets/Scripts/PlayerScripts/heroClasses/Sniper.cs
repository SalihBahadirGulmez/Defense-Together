using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Character
{
    public Sniper(int health = 500, int mana = 100, int damage = 30, int attackSpeed = 2, float attackRange = 15f, float movementSpeed = 5.0f, float rotationSpeed = 20f, int armor = 2, int magicResistance = 10, float healthRegen = 1, float manaRegen = 1f, float projectileSpeed = 10f)
        : base(health, mana, damage, attackSpeed, attackRange, movementSpeed, rotationSpeed, armor, magicResistance, healthRegen, manaRegen, projectileSpeed)
    {

    }
}
