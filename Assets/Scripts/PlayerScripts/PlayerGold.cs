using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGold : MonoBehaviour
{
    private int gold;

    private Text goldText2D;
    void Start()
    {
        goldText2D = transform.Find("2D Canvas/2D Gold Text").GetComponent<Text>();
        goldText2D.text = gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WinGold(int _gold)
    {
        gold += _gold;
        goldText2D.text = gold.ToString();
    }
}
