using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Character
{
    public Base(int health = 100, int mana = 0, int damage = 0, int attackSpeed = 0, float attackRange = 0f, float movementSpeed = 0f, float rotationSpeed = 0f, int armor = 5, int magicResistance = 100, float healthRegen = 0, float manaRegen = 0f)
         : base(health, mana, damage, attackSpeed, attackRange, movementSpeed, rotationSpeed, armor, magicResistance, healthRegen, manaRegen)
    {

    }
}
