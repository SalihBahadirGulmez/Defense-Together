using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    private Slider healthSlider2D;
    private Slider healthSlider3D;
    private Slider manaSlider2D;
    private Slider manaSlider3D;
    private Stats statsScript;

    public Text manaText2D;

    public Text healthText2D;
    // Start is called before the first frame update
    void Start()
    {
        statsScript = GetComponent<Stats>();

        healthSlider2D = transform.Find("2D Canvas/2D Health Bar(Slider)").GetComponent<Slider>();
        healthSlider3D = transform.Find("3D Canvas/3D Health Bar(Slider)").GetComponent<Slider>();
        healthText2D = transform.Find("2D Canvas/2D Health Text").GetComponent<Text>();
        if(gameObject.tag != "building")
        {
            manaSlider2D = transform.Find("2D Canvas/2D Mana Bar(Slider)").GetComponent<Slider>();
            manaSlider3D = transform.Find("3D Canvas/3D Mana Bar(Slider)").GetComponent<Slider>();
            manaText2D = transform.Find("2D Canvas/2D Mana Text").GetComponent<Text>();
        }
        
        
        if (gameObject.tag != "Player" || !gameObject.GetComponent<RecievedMovement>().enabled)
        {
            transform.Find("2D Canvas").gameObject.SetActive(false);
        }

        healthSlider2D.maxValue = statsScript.character.health;
        healthSlider2D.value = statsScript.character.currentHealth;
        healthSlider3D.maxValue = statsScript.character.health;
        healthSlider3D.value = statsScript.character.currentHealth;
        healthText2D.text = statsScript.character.currentHealth.ToString() + "/" + statsScript.character.health.ToString();
        if (gameObject.tag != "building")
        {
            manaSlider2D.maxValue = statsScript.character.mana;
            manaSlider2D.value = statsScript.character.currentMana;
            manaSlider3D.maxValue = statsScript.character.mana;
            manaSlider3D.value = statsScript.character.currentMana;
            manaText2D.text = statsScript.character.currentMana.ToString() + "/" + statsScript.character.mana.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void RegenerateHealthUI(int _currentHealth, int _health)
    {
        healthSlider3D.value = _currentHealth;
        healthSlider2D.value = _currentHealth;
        healthText2D.text = _currentHealth.ToString() + "/" + _health.ToString();
    }
    public void RegenerateManaUI(int _currentMana, int _mana)
    {
        manaSlider3D.value = _currentMana;
        manaSlider2D.value = _currentMana;
        manaText2D.text = _currentMana.ToString() + "/" + _mana.ToString();
    }

    public void TakeDamageUI(int _currentHealth, int _health)
    {
        healthSlider2D.value = _currentHealth;
        healthSlider3D.value = _currentHealth;
        healthText2D.text = _currentHealth.ToString() + "/" + _health.ToString();
    }
    public void ManaLoseUI(int _currentMana, int _mana)
    {
        manaSlider2D.value = _currentMana;
        manaSlider3D.value = _currentMana;
        manaText2D.text = _currentMana.ToString() + "/" + _mana.ToString();
    }
}
