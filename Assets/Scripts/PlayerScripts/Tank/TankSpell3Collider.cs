using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpell3Collider : MonoBehaviour
{
    public TankSpells tankSpellsSicript;

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
            StartCoroutine(Shield(other));
        }
    }

    IEnumerator Shield(Collider _player)
    {
        _player.GetComponent<Stats>().character.armor += (int)tankSpellsSicript.ability3.buff;
        yield return new WaitForSecondsRealtime(tankSpellsSicript.ability3.duration);
        _player.GetComponent<Stats>().character.armor -= (int)tankSpellsSicript.ability3.buff;
    }
}
