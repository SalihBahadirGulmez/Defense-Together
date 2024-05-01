using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    public Slider healthSlider2D;
    public Slider healthSlider3D;
    public Slider manaSlider2D;
    public Slider manaSlider3D;
    private Stats statsScript;

    private float tempMana;
    // Start is called before the first frame update
    void Start()
    {
        statsScript = GetComponent<Stats>();

        manaSlider2D = transform.Find("2D Canvas/2D Mana Bar(Slider)").GetComponent<Slider>();
        manaSlider3D = transform.Find("3D Canvas/3D Mana Bar(Slider)").GetComponent<Slider>();

        if (gameObject.tag != "Player" || !gameObject.GetComponent<RecievedMovement>().enabled)
        {
            transform.Find("2D Canvas").gameObject.SetActive(false);
        }

        healthSlider2D.maxValue = statsScript.character.health;
        healthSlider2D.value = statsScript.character.currentHealth;
        healthSlider3D.maxValue = statsScript.character.health;
        healthSlider3D.value = statsScript.character.currentHealth;
        manaSlider2D.maxValue = statsScript.character.mana;
        manaSlider2D.value = statsScript.character.currentMana;
        manaSlider3D.maxValue = statsScript.character.mana;
        manaSlider3D.value = statsScript.character.currentMana;
    }
    // Update is called once per frame
    void Update()
    {
        RegenerateMana();
    }
    private void RegenerateMana()
    {
        if(statsScript.character.currentMana < statsScript.character.mana)
        {
            tempMana = statsScript.character.manaRegen * Time.deltaTime;
            if (tempMana >= 1)
            {
                statsScript.character.currentMana++; //current mananýn deðerini kontrol etmek için Mathf.Clamp fonksiyonu kullanýlabilir
                tempMana = 0;
                manaSlider3D.value = statsScript.character.currentMana;
                manaSlider2D.value = statsScript.character.currentMana;
            }
        }
    }
}
