using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSpell1Collider : MonoBehaviour
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
            StartCoroutine(other.GetComponent<CreepAI>().Stunned(supportSpellsSicript.ability1.duration, other.gameObject));
            other.GetComponent<Stats>().TakeDamage(supportSpellsSicript.ability1.damage, player);
        }
    }
}
