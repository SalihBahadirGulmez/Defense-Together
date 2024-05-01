using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;
using System.Linq;

public class RecievedMovement : MonoBehaviour
{
    NavMeshAgent agent;
    bool needPath = true;
    List<Vector3> _path = new List<Vector3>();

    private int maxCornetLength = 0;
    private Quaternion rotationToLookAt;
    private float rotateAngle;
    private Vector3 tempPath;
    private bool _followObj = false;
    private Vector3 targetPoint;

    private Ray ray;
    private RaycastHit hit;
    private bool hasHit;
    private float totalRadius;

    public GameObject targetObj;
    private Stats statsScript;

    Animator heroAnimation;

    private int i;
    private Collider[] enemyObjectsCollider;
    private float tempDistance;
    private float distance = 10;
    private bool pressedButtonA;
    private bool findAndSetTarget;

    private int layerMask;
    private int layerNumber = 5;

    private bool spellAnimation;

    AnimationClip[] clips;

    Abilities abilitiesScript;
    void Start()
    {
        layerMask = 1 << layerNumber;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        tempPath = transform.position;
        heroAnimation = gameObject.GetComponent<Animator>();
        statsScript = gameObject.GetComponent<Stats>();
        abilitiesScript = gameObject.GetComponent<Abilities>();


        clips = heroAnimation.runtimeAnimatorController.animationClips;

    }
    void Update()
    {
        AttackWithButtonA();
        if(findAndSetTarget)
        {
            FindAndSetTarget();
        }

        if (Input.GetMouseButtonDown(1))
        {
            pressedButtonA = false;
            ClickToMove();
            needPath = true;
        }

        if (needPath && agent.path.corners.Length > 1 && _path.Count == 0)
        {
            path();
        }

        if (needPath && agent.path.corners.Length > maxCornetLength)
        {
            maxCornetLength = agent.path.corners.Length;
            newPath();
        }

        if (needPath && agent.path.corners.Length > 1 && _path.Count > 0 && agent.path.corners[1] != _path[0])
        {
            newPath();
        }

        if (_path.Count > 0)
        {
            Move();
        }

        if (_followObj)
        {
            if (targetObj == null)
            {
                _followObj = false;
                heroAnimation.SetBool("Attack", false);
                heroAnimation.SetBool("Run", false);
            }
            else
            {
                followObj();
            }
        }
    }

    private void ClickToMove()
    {
        if (!spellAnimation)
        {
            if (agent.hasPath)
            {
                _path.Clear();
                agent.ResetPath();
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask);
            if (hasHit)
            {
                StopAllAnimations();
                heroAnimation.SetBool("Run", true);
                if (hit.transform.tag == "enemy")
                {
                    if (targetObj != hit.transform.gameObject) heroAnimation.SetBool("Attack", false);
                    targetObj = hit.transform.gameObject;
                    _followObj = true;
                    totalRadius = targetObj.GetComponent<CapsuleCollider>().radius * targetObj.transform.localScale.x  + statsScript.character.attackRange;
                    return;
                }
                else if (hit.transform.tag == "friend")
                {
                    _followObj = true;
                    targetObj = hit.transform.gameObject;
                    totalRadius = targetObj.GetComponent<CapsuleCollider>().radius * targetObj.transform.localScale.x + statsScript.character.hitBoxSize;
                }
                else
                {
                    _followObj = false;
                    SetDestination(hit.point);
                }
            }
        }
    }

    private void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    void Move()
    {
        if (_path[0] == transform.position)
        {
            _path.Clear();
            return;
        }
        if (tempPath != _path[0]) //tekrar tekrar döneceði noktayý hesaplamasýn diye gideceði yer kontrol ediliyor
        {
            rotationToLookAt = Quaternion.LookRotation(_path[0] - transform.position);
            tempPath = _path[0];
        }
        rotateAngle = rotationToLookAt.eulerAngles.y - transform.rotation.eulerAngles.y;
        if (Math.Abs(rotateAngle) > 5)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, Time.deltaTime * statsScript.character.rotationSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _path[0], Time.deltaTime * statsScript.character.movementSpeed);
            agent.nextPosition = transform.position;
        }
        if (Math.Round(transform.position.x, 3) == Math.Round(_path[0].x, 3) && Math.Round(transform.position.z, 3) == Math.Round(_path[0].z, 3))
        {
            agent.nextPosition = transform.position;
            _path.RemoveAt(0);
        }
        if (_path.Count == 0 && !_followObj)
        {
            heroAnimation.SetBool("Run", false);
            needPath = false;
            maxCornetLength = 0;
            findAndSetTarget = false;
        }
    }

    private void newPath()
    {
        _path.Clear();
        path();
    }

    private void path()
    {
        for (int i = 1; i < agent.path.corners.Length; i++)
        {
            _path.Add(agent.path.corners[i]);
        }
    }

    private void followObj()
    {
        //todo alttaki denklemi kýsalt, statikdurumlarda tekrar tekrar hesap yapmasýna gerek yok 
        targetPoint = targetObj.transform.position + ((transform.position - targetObj.transform.position).normalized) * totalRadius;
        if (Vector3.Distance(targetPoint, targetObj.transform.position) >= Vector3.Distance(transform.position, targetObj.transform.position) || Vector3.Distance(targetPoint, transform.position) <= agent.stoppingDistance)
        {
            _path.Clear();
            agent.ResetPath();
            rotationToLookAt = Quaternion.LookRotation(targetObj.transform.position - transform.position);
            rotateAngle = rotationToLookAt.eulerAngles.y - transform.rotation.eulerAngles.y;
            if (Math.Abs(rotateAngle) > 5)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, Time.deltaTime * statsScript.character.rotationSpeed);
            }
            else if (targetObj.transform.tag == "enemy")
            {
                heroAnimation.SetBool("Run", false);
                heroAnimation.SetBool("Attack", true);
            }
            return;
        }
        heroAnimation.SetBool("Run", true);
        heroAnimation.SetBool("Attack", false);
        SetDestination(targetPoint);
    }

    private void FindAndSetTarget()
    {
        enemyObjectsCollider = Physics.OverlapSphere(transform.position, distance);
        for (i = 0; i < enemyObjectsCollider.Length; i++)
        {
            if (enemyObjectsCollider[i].gameObject.activeSelf && enemyObjectsCollider[i].gameObject.tag == "enemy")
            {
                tempDistance = Vector3.Distance(transform.position, enemyObjectsCollider[i].gameObject.transform.position);
                if (distance > tempDistance)
                {
                    distance = tempDistance;
                    targetObj = enemyObjectsCollider[i].gameObject;
                }
            }
        }
        if (targetObj != null)
        {
            totalRadius = targetObj.GetComponent<CapsuleCollider>().radius * targetObj.transform.localScale.x + statsScript.character.attackRange;
            _followObj = true;
            findAndSetTarget = false;
        }
        distance = 10;
    }

    private void AttackWithButtonA()
    {
        if (Input.GetKeyDown(KeyCode.A)) pressedButtonA = true;
        if (pressedButtonA)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pressedButtonA = false;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask);
                if (hasHit)
                {
                    heroAnimation.SetBool("Run", true);

                    if (hit.transform.tag == "enemy")
                    {
                        if (targetObj != hit.transform.gameObject) heroAnimation.SetBool("Attack", false);
                        targetObj = hit.transform.gameObject;
                        _followObj = true;
                        totalRadius = hit.transform.GetComponent<CapsuleCollider>().radius * hit.transform.localScale.x + statsScript.character.attackRange;
                        return;
                    }
                    _followObj = false;
                    SetDestination(hit.point);

                    heroAnimation.SetBool("Attack", false);
                    findAndSetTarget = true;
                    needPath = true;
                }
            }
        }
    }

    public void Spell(string skillName) //Abilities de kullanýlýyor
    {
        pressedButtonA = false;
        _path.Clear();
        agent.ResetPath();
        StopAllAnimations();
        heroAnimation.SetBool(skillName, true);
        StartCoroutine(SpellLength(skillName));
    }
    IEnumerator SpellLength(string clipName)
    {
        spellAnimation = true;
        abilitiesScript.canUseSpell = false;

        for (i = 0; i < clips.Length; i++)
        {
            if (clips[i].name == clipName)
            {
                yield return new WaitForSecondsRealtime(clips[i].length);
            }
        }
        spellAnimation = false;
        abilitiesScript.canUseSpell = true;
    }
    private void StopAllAnimations()
    {
        heroAnimation.SetBool("Run", false);
        heroAnimation.SetBool("Attack", false);
        heroAnimation.SetBool("Skill1", false);
        heroAnimation.SetBool("Skill2", false);
        heroAnimation.SetBool("Skill3", false);
    }
}