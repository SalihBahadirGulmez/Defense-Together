using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCalculator : MonoBehaviour
{
    private int baseExp = 10;
    private int expRadius = 10;
    private float distance;
    private GameObject[] players;
    List<int> indexOfGameObjects = new List<int>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void releaseExp()//stats ta düþman ölünce kullanýlýyor
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
            distance = Vector3.Distance(transform.position, players[i].transform.position);
            if(distance <= expRadius)
            {
                indexOfGameObjects.Add(i);
            }
        }
        foreach(int i in indexOfGameObjects)
        {
            players[i].GetComponent<PlayerExp>().gainExp(baseExp / indexOfGameObjects.Count);
        }
    }
}
