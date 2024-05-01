using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpell1Collider : MonoBehaviour
{
    public TankSpells tankSpellsSicript;
    public GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.GetComponent<Stats>().TakeDamage(tankSpellsSicript.ability1.damage, player);
        }
    }
}
