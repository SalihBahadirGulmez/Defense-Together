using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSpell2Collider : MonoBehaviour
{
    public SupportSpells supportSpellsSicript;
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
            StartCoroutine(other.GetComponent<CreepAI>().Slowed(supportSpellsSicript.ability2.buff, supportSpellsSicript.ability2.duration, other.gameObject));
            StartCoroutine(other.GetComponent<Stats>().DamageEverySec(supportSpellsSicript.ability2.damage, supportSpellsSicript.ability2.duration, player));
        }
    }
    
}
