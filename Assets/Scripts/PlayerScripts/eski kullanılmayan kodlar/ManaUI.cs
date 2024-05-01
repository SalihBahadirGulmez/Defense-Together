using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    public Slider manaSlider2D;
    public Slider manaSlider3D;
    private Stats statsScript;
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

        manaSlider2D.maxValue = statsScript.character.mana;
        manaSlider2D.value = statsScript.character.currentMana;
        manaSlider3D.maxValue = statsScript.character.mana;
        manaSlider3D.value = statsScript.character.currentMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
