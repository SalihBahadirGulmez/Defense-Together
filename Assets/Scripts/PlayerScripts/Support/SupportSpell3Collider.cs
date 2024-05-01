using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSpell3Collider : MonoBehaviour
{
    public SupportSpells supportSpellsSicript;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Heal(other));
        }
    }

    IEnumerator Heal(Collider _player)
    {
        _player.GetComponent<Stats>().character.healthRegen += (int)supportSpellsSicript.ability3.buff;
        yield return new WaitForSecondsRealtime(supportSpellsSicript.ability3.duration);
        if(_player != null) _player.GetComponent<Stats>().character.healthRegen -= (int)supportSpellsSicript.ability3.buff;
    }
}
