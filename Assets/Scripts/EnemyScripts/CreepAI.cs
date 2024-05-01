using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] players;
    private GameObject targetPlayer;
    private int i;
    private int maxDistanceToSetTarget = 5;
    private int maxDistanceToFollowTarget = 10;
    private float distance;
    private float tempDistance;
    private GameObject mainTarget;
    private float totalRadiusMainTarget;
    private float totalRadiusPlayer;
    private Stats statsScript;
    private Vector3 targetPoint;
    public GameObject targetObj; //basicAtacktan eriþiliyor o yüzden public olmalý
    private Animator creepAnimation;

    public GameObject player;

    public Coroutine slowCoroutine;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
        distance = maxDistanceToSetTarget;
        mainTarget = GameObject.Find("Base");
        statsScript = GetComponent<Stats>();
        totalRadiusMainTarget = mainTarget.transform.GetComponent<CapsuleCollider>().radius * mainTarget.transform.localScale.x + statsScript.character.attackRange;
        totalRadiusPlayer = player.transform.GetComponent<CapsuleCollider>().radius * player.transform.localScale.x + statsScript.character.attackRange;
        creepAnimation = gameObject.GetComponent<Animator>();
        agent.speed = statsScript.character.movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (FollowTarget())
        {
            targetPoint = targetPlayer.transform.position + ((transform.position - targetPlayer.transform.position).normalized) * totalRadiusPlayer;
            agent.SetDestination(targetPoint);
        }
        else FindAndSetTarget();
        if (agent.isStopped) //stun lar için
        {
            creepAnimation.SetBool("Run", false);
            creepAnimation.SetBool("Attack", false);
        }
        else if (Vector3.Distance(targetPoint, transform.position) <= agent.stoppingDistance)
        {
            transform.LookAt(targetObj.transform);
            creepAnimation.SetBool("Run", false);
            creepAnimation.SetBool("Attack", true);
        }
        else
        {
            creepAnimation.SetBool("Run", true);
            creepAnimation.SetBool("Attack", false);
        }

    }
    private void FindAndSetTarget()
    {
        for (i = 0; i < players.Length; i++)
        {
            if (players[i].activeSelf)
            {
                tempDistance = Vector3.Distance(transform.position, players[i].transform.position);
                if (distance > tempDistance && tempDistance < maxDistanceToSetTarget)
                {
                    distance = tempDistance;
                    targetPlayer = players[i];
                }
            }
        }
        targetObj = targetPlayer;
        if(targetPlayer == null)
        {
            targetObj = mainTarget;
            targetPoint = mainTarget.transform.position + ((transform.position - mainTarget.transform.position).normalized) * totalRadiusMainTarget;
            agent.SetDestination(targetPoint);
        }
        distance = maxDistanceToSetTarget;
    }
    private bool FollowTarget()
    {
        if (targetPlayer != null && targetPlayer.activeSelf && Vector3.Distance(transform.position, targetPlayer.transform.position) < maxDistanceToFollowTarget)
        {
            return true;
        }
        else
        {
            targetPlayer = null;
            return false;
        }
    }

    public IEnumerator Stunned(float time, GameObject thisGameObj)
    {
        agent.isStopped = true;
        creepAnimation.SetBool("Run", false);
        creepAnimation.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(time);
        if (thisGameObj != null) agent.isStopped = false;
    }
    public IEnumerator Slowed(float amount, float time, GameObject thisGameObj)
    {
        statsScript.character.movementSpeed *= amount;
        agent.speed *= amount;
        yield return new WaitForSecondsRealtime(time);
        if(thisGameObj != null)
        {
            statsScript.character.movementSpeed /= amount;
            agent.speed /= amount;
        }
    }

}
