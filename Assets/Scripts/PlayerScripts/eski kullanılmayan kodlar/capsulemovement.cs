using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using WebSocketSharp.Net;

public class capsulemovement : MonoBehaviour
{
    NavMeshAgent agent;
    bool needPath = true;
    List<Vector3> _path = new List<Vector3>(); 
    public float velocity = 5.0f;
    public float angularVelocity = 10.0f;
    private int maxCornetLength = 0;
    private Quaternion rotationToLookAt;
    private float rotateAngle;
    private float secondRotateAngle;
    private Vector3 tempPath;
    private bool _followObj = false;
    private Vector3 targetPoint;


    private Ray ray;
    private RaycastHit hit;
    private bool hasHit;
    private float totalRadius;


    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        tempPath = transform.position;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Invoke("ClickToMove", 0.1f);
            ClickToMove();
            needPath = true;
        }

        if (needPath && agent.path.corners.Length > 1 && _path.Count == 0)
        {
             path();
        }

        if(needPath && agent.path.corners.Length > maxCornetLength)
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
            move();
        }
        if (_followObj)
        {
            followObj();
        }
    }

    private void ClickToMove()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            if(hit.transform.tag == "enemy" || hit.transform.tag == "friend")
            {
                _followObj = true;
                totalRadius = hit.transform.GetComponent<CapsuleCollider>().radius + transform.GetComponent<CapsuleCollider>().radius;
            }
            else
            {
                _followObj = false;
                SetDestination(hit.point);

            }
        }
    }

    private void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }
    void move() 
    {
        if (_path[0] == transform.position)
        {
            _path.Clear();
            return;
        }        
        if(tempPath != _path[0])
        {
            rotationToLookAt = Quaternion.LookRotation(_path[0] - transform.position);
            tempPath = _path[0];
        }
        rotateAngle = rotationToLookAt.eulerAngles.y - transform.rotation.eulerAngles.y;
        if (Math.Abs(rotateAngle) > 5)
        {           
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, Time.deltaTime * angularVelocity);
            secondRotateAngle = rotationToLookAt.eulerAngles.y - transform.rotation.eulerAngles.y;
            if (Math.Abs(rotateAngle) < 12 || Math.Abs(secondRotateAngle) > Math.Abs(rotateAngle))
            {
                transform.LookAt(_path[0]);
            }
            agent.nextPosition = transform.position;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _path[0],Time.deltaTime * velocity);// belli bir sayýnýn alyýndayken direkt pozisyonu eþitle yoksa sonda uzun bir süre bekliyor
            agent.nextPosition = transform.position;
        }
        if (Math.Round(transform.position.x,3) == Math.Round(_path[0].x,3) && Math.Round(transform.position.z, 3) == Math.Round(_path[0].z, 3)) //2->3
        {
            agent.nextPosition = transform.position;
            _path.RemoveAt(0);
        }
        if (_path.Count == 0 && hit.transform.tag != "enemy")
        {
            needPath = false;
            maxCornetLength = 0;
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
        // alttaki denklemi kýsalt, static durumlarda tekrar tekrar hesap yapmasýna gerek yok 
        targetPoint = hit.transform.position + ((transform.position - hit.transform.position) / Vector3.Distance(transform.position, hit.transform.position)) * totalRadius;
        SetDestination(targetPoint);
    }
}
