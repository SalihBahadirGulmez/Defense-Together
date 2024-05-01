using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicAtack : MonoBehaviour
{

    private RecievedMovement movementScript;
    private Stats statsScript;
    private CreepAI creepAIScript;

    void Start()
    {
        if(gameObject.tag == "Player") movementScript = GetComponent<RecievedMovement>();  //todo en sonoyun tamamlandýðýnda movement ve creep scripte gerek olmayabilir.
        else creepAIScript = GetComponent<CreepAI>();
      
        statsScript = GetComponent<Stats>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void BasicAtackFunc()
    {
        if(gameObject != null)
        {
            if (gameObject.tag == "Player") movementScript.targetObj.GetComponent<Stats>().TakeDamage(statsScript.character.damage, gameObject);
            else creepAIScript.targetObj.GetComponent<Stats>().TakeDamage(statsScript.character.damage);
        }
    }
}
