using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : Character
{
    public Support(int health = 750, int mana = 200, int damage = 10, int attackSpeed = 2, float attackRange = 10f, float movementSpeed = 5.0f, float rotationSpeed = 20f, int armor = 1, int magicResistance = 10, float healthRegen = 1, float manaRegen = 2f, float projectileSpeed = 3f)
        : base(health, mana, damage, attackSpeed, attackRange, movementSpeed, rotationSpeed, armor, magicResistance, healthRegen, manaRegen, projectileSpeed)
    {

    }
}
