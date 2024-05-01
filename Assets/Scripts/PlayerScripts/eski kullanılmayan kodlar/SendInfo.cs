using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SendInfo : MonoBehaviour
{
    Tank hero = new Tank();
    Vector3 newPosition;
    Vector3 unitVector;
    float distance;

    Animator heroAnimation;

    RaycastHit hit;

    GameObject hitObj;

    void Start()
    {
        heroAnimation = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) )
            {
                hitObj = hit.transform.gameObject;
                //distance = Vector3.Distance(hit.transform.position, transform.position);
                if (hit.transform.tag == "ground")//|| distance - hit.transform.GetComponent<CapsuleCollider>().radius > hero.atackRange
                {
                    heroAnimation.SetBool("Atack", false);
                    heroAnimation.SetBool("Skill", false);

                    //gameObject.GetComponent<PhotonView>().RPC("RecievedMove", RpcTarget.All, hitObj, hit.transform.tag, 0f, hit.point);

                }
                else if (hit.transform.tag == "friend")//&& distance > hero.atackRange + hit.transform.GetComponent<CapsuleCollider>().radius
                {
                    heroAnimation.SetBool("Atack", false);
                    heroAnimation.SetBool("Skill", false);
                    //unitVector = (hit.transform.position - transform.position) / distance;
                    //newPosition = hit.transform.position - unitVector * (hit.transform.GetComponent<CapsuleCollider>().radius + hero.hitBoxSize);
                    gameObject.GetComponent<PhotonView>().RPC("RecievedMove", RpcTarget.All, hit.transform.gameObject, hit.transform.tag, hit.transform.GetComponent<CapsuleCollider>().radius, hit.point);

                }
                else//"enemy"
                {
                    heroAnimation.SetBool("Atack", false);
                    heroAnimation.SetBool("Skill", false);
                    //unitVector = (hit.transform.position - transform.position) / distance;
                    //newPosition = hit.transform.position - unitVector * (hit.transform.GetComponent<CapsuleCollider>().radius + hero.atackRange);
                    gameObject.GetComponent<PhotonView>().RPC("RecievedMove", RpcTarget.All, hit.transform.gameObject, hit.transform.tag, hit.transform.GetComponent<CapsuleCollider>().radius, hit.point);

                }
            }
        }
    }
}
