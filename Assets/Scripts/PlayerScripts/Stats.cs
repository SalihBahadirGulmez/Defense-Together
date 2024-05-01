using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stats : MonoBehaviour
{
    public Character character;
    private Animator anim;
    private ExpCalculator expCalculatorScript;
    private CharacterUI CharacterUIScript;
    private GameObject lastHit;
    private int gold;
    private GameManager gameManagerScript;

    public Coroutine damageCoroutine;

    private float tempHealth;
    private float tempMana;
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        switch (gameObject.name)
        {
            case "Tank":
                character = new Tank();
                break;
            case "Sniper":
                character = new Sniper();
                break;
            case "Support":
                character = new Support();
                break;
            case "BasicCreep":
                character = new Creep();
                expCalculatorScript = gameObject.GetComponent<ExpCalculator>();
                GoldCalculator();
                break;
            case "Base":
                character = new Base();
                break;
            default:
                // code block
                break;
        }
        if (gameObject.GetComponent<Animator>() != null)//todo bunu deðiþen atack speedlerde tekrar düzenle atackspeedi arttýran fonksiton olabilir
        {
            anim = GetComponent<Animator>();
            anim.SetFloat("Attack Speed", character.attackSpeed);
        }
        CharacterUIScript = gameObject.GetComponent<CharacterUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (character.currentHealth <= 0)
        {
            if (expCalculatorScript != null)
            {
                lastHit.GetComponent<PlayerGold>().WinGold(gold);
                expCalculatorScript.releaseExp();
                gameManagerScript.ReduceNumberOfEnemy();
            }
            if (gameObject.tag == "Player") gameObject.SetActive(false);
            else if (gameObject.name == "Base")
            {
                gameManagerScript.StopGame();

            } else Destroy(gameObject);
        }
        RegenerateHealth();
        if (gameObject.tag != "building")
        {
            RegenerateMana();
        }
    }
    public void TakeDamage(int _damage, GameObject _player = null)//basicAtack ve projectileMovement ta kullanýlýyor
    {
        lastHit = _player;
        _damage -= character.armor;
        if (_damage > 0)
        {
            character.currentHealth -= _damage;
            CharacterUIScript.TakeDamageUI(character.currentHealth, character.health);
        }
    }
    public IEnumerator DamageEverySec(int damage, float duration, GameObject attacker)
    {
        for (int i = 0; i < duration; i++)
        {
            TakeDamage(damage, attacker);
            yield return new WaitForSecondsRealtime(1);
        }
    }
    public void ManaLose(int _mana)
    {
        character.currentMana -= _mana;
        CharacterUIScript.ManaLoseUI(character.currentMana, character.mana);
    }
    private void GoldCalculator()
    {
        gold = (int)(character.currentHealth / 10);
    }
    private void RegenerateHealth()
    {
        if (character.currentHealth < character.health)
        {
            tempHealth += character.healthRegen * Time.deltaTime;
            if (tempHealth >= 1)
            {
                character.currentHealth++; 
                tempHealth = 0;
                CharacterUIScript.RegenerateHealthUI(character.currentHealth, character.health);
            }
        }
    }
    private void RegenerateMana()
    {
        if (character.currentMana < character.mana)
        {
            tempMana += character.manaRegen * Time.deltaTime;
            if (tempMana >= 1)
            {
                character.currentMana++; //current mananýn deðerini kontrol etmek için Mathf.Clamp fonksiyonu kullanýlabilir
                tempMana = 0;
                CharacterUIScript.RegenerateManaUI(character.currentMana, character.mana);
            }
        }
    }
}
