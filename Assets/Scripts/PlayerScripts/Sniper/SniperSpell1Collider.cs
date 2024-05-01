using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSpell1Collider : MonoBehaviour
{
    public SniperSpells sniperSpellsSicript;
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
            other.GetComponent<Stats>().damageCoroutine = StartCoroutine(other.GetComponent<Stats>().DamageEverySec(sniperSpellsSicript.ability1.damage, sniperSpellsSicript.ability1.duration, player));
            other.GetComponent<CreepAI>().slowCoroutine = StartCoroutine(other.GetComponent<CreepAI>().Slowed(sniperSpellsSicript.ability1.buff, sniperSpellsSicript.ability1.duration, other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            StopCoroutine(other.GetComponent<Stats>().damageCoroutine);
            StopCoroutine(other.GetComponent<CreepAI>().slowCoroutine);
        }
    }
}
