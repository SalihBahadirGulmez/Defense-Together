using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    public int health;
    public int currentHealth;
    public int mana;
    public int currentMana;
    public int damage;
    public int attackSpeed;
    public float attackRange;
    public float projectileSpeed;
    public float movementSpeed;
    public float rotationSpeed;
    public int armor;
    public int magicResistance;
    public float walkRange = 0.001f;
    public float hitBoxSize = 0.5f;
    public float manaRegen;
    public float healthRegen;
    public Character(int _health,  int _mana,  int _damage, int _attackSpeed, float _attackRange, float _movementSpeed, float _rotationSpeed, int _armor, int _magicResistance, float _healthRegen, float _manaRegen, float _projectileSpeed = 0)
    {                   //1           //2          //3          //4                //5                //6                   //7                 //8         //9                     //10                //11              //12
        health = _health;
        currentHealth = _health;
        healthRegen = _healthRegen;
        mana = _mana;
        currentMana = _mana;
        manaRegen = _manaRegen;
        damage = _damage;
        attackSpeed = _attackSpeed;
        attackRange = _attackRange;
        projectileSpeed = _projectileSpeed;
        movementSpeed = _movementSpeed;
        rotationSpeed = _rotationSpeed;
        armor = _armor;
        magicResistance = _magicResistance;
    }
    public void Atack(int _distance)
    {
        if(attackRange >= _distance)
        {
            //atack animation

        }
    }
}
