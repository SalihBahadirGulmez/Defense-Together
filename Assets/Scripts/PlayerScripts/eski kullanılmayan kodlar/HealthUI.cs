using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    public Slider healthSlider2D;
    public Slider healthSlider3D;
    private Stats statsScript;
    // Start is called before the first frame update
    void Start()
    {
        statsScript = GetComponent<Stats>();

        healthSlider2D = transform.Find("2D Canvas/2D Health Bar(Slider)").GetComponent<Slider>();
        healthSlider3D = transform.Find("3D Canvas/3D Health Bar(Slider)").GetComponent<Slider>();

        if(gameObject.tag != "Player" || !gameObject.GetComponent<RecievedMovement>().enabled)
        {
            transform.Find("2D Canvas").gameObject.SetActive(false);
        }

        healthSlider2D.maxValue = statsScript.character.health;
        healthSlider2D.value = statsScript.character.currentHealth;
        healthSlider3D.maxValue = statsScript.character.health;
        healthSlider3D.value = statsScript.character.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
