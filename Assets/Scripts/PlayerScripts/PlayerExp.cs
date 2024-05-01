using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    private int currentExp = 0;
    private int maxExp = 30;
    private float maxExpMultiplier = 1.5f;
    public int tempExp;

    private Text levelText2D;
    private Text levelText3D;
    private Slider expSlider2D;

    private int currentLevel = 1;//stats scripte de koyulabilir
    void Start()
    {
        levelText3D = transform.Find("3D Canvas/3D Level Text").GetComponent<Text>();
        levelText2D = transform.Find("2D Canvas/2D Level Image/2D Level Text").GetComponent<Text>();
        expSlider2D = transform.Find("2D Canvas/2D Level Image/Exp Slider").GetComponent<Slider>();
        expSlider2D.value = currentExp;
        expSlider2D.maxValue = maxExp;
        levelText2D.text = currentLevel.ToString();
        levelText3D.text = currentLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gainExp(int _tempExp)//expCalculator da kullanýlýyor
    {
        if (currentExp + _tempExp < maxExp)
        {
            currentExp += _tempExp;
            expSlider2D.value = currentExp;
        }
        else
        {
            currentExp = currentExp + _tempExp - maxExp;
            maxExp = (int)(maxExp * maxExpMultiplier);
            expSlider2D.maxValue = maxExp;
            expSlider2D.value = currentExp;
            currentLevel++;
            levelText2D.text = currentLevel.ToString();
            levelText3D.text = currentLevel.ToString();
        }
    }
}
