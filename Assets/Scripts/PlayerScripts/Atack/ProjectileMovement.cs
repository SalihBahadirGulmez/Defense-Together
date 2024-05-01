using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private GameObject targetEnemy;
    private bool isAlive = true;
    private Vector3 targetLastPos;
    private float stoppingDistance = 0.5f;
    float distace;
    float remainingDistance;
    private Stats statsScript;
    private GameObject player;
    private void Awake()
    {
        
    }
    void Start()
    {
        if (transform.parent.tag == "Player")
        {
            player = transform.parent.gameObject;
            targetEnemy = transform.parent.GetComponent<RecievedMovement>().targetObj;
        }
        //else if(transform.parent.tag == "enemy")
        //{
        //    if (targetEnemy != null)
        //    {
        //        targetEnemy = creepAIScript.targetObj;
        //    }
        //}
        statsScript = transform.parent.GetComponent<Stats>();
        if(targetEnemy == null)
        {
            Destroy(gameObject);
        }
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAlive)
        {
            if (targetEnemy != null) 
            {
                transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, statsScript.character.projectileSpeed * Time.deltaTime);
                targetLastPos = targetEnemy.transform.position;
            }
            else
            {
                isAlive = false;
                distace = Vector3.Distance(transform.position, targetLastPos);
                remainingDistance = distace / (statsScript.character.projectileSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetLastPos, 1 / remainingDistance);
            remainingDistance -= 1;
            if (Vector3.Distance(transform.position, targetLastPos) <= stoppingDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == targetEnemy)
        {
            targetEnemy.GetComponent<Stats>().TakeDamage(statsScript.character.damage , player);
            Destroy(gameObject);
        }
    }
}
