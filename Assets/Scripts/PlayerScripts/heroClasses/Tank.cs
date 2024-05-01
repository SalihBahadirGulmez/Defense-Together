using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tank : Character
{
    public Tank(int health = 1000, int mana = 150, int damage = 20, int attackSpeed = 1, float attackRange = 0.5f, float movementSpeed = 5.0f, float rotationSpeed = 30f, int armor = 5, int magicResistance = 20, float healthRegen = 3, float manaRegen = 0.5f)
        : base(health, mana, damage, attackSpeed, attackRange, movementSpeed, rotationSpeed, armor, magicResistance, healthRegen, manaRegen)
    { 

    }

 
}
