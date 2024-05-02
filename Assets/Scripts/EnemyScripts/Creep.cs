using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : Character
{
    public Creep(int health = 100, int mana = 0, int damage = 10, int attackSpeed = 2, float attackRange = 0.2f, float movementSpeed = 3.0f, float rotationSpeed = 10f, int armor = 1, int magicResistance = 0, float healthRegen = 1, float manaRegen = 0f)
        : base(health, mana, damage, attackSpeed, attackRange, movementSpeed, rotationSpeed, armor, magicResistance, healthRegen, manaRegen)
    {

    }
}
