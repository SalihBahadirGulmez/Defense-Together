using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell 
{
    public string type;
    public int damage;
    public int mana;
    public int cooldown;
    public float buff;
    public float duration;

    public Spell(string _type, int _damage, int _mana, int _cooldown, float _buff, float _duration)
    {
        type = _type;
        damage = _damage;  
        mana = _mana;   
        cooldown = _cooldown;
        buff = _buff;
        duration= _duration;
    }
}
