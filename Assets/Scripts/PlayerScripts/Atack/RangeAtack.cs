using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAtack : MonoBehaviour
{

    //private CreepAI creepAIScript; creplerin en yak�ndaki hedefe sald�rd��� script
    GameObject projectile;

    private Vector3 projectilePos = new Vector3(-0.15f, 1, 2.15f); 


    //todo start fonksiyonunda swtich case gibi bir yap�yla �retilecek range atacklar�n prefablerini hero ad�na crep ad�na g�re e�le�tir.
    void Start()
    {
        //if (gameObject.tag == "Player")
        //{
        //    movementScript = GetComponent<RecievedMovement>();

        //}
        //statsScript = GetComponent<Stats>();
        //if (gameObject.tag == "enemy")
        //{
        //    creepAIScript = GetComponent<CreepAI>();
        //}

        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void CreateProjectile()
    {
        projectile = PhotonNetwork.Instantiate("Support Projectile", transform.position + projectilePos, Quaternion.identity);
        projectile.transform.RotateAround(transform.position, Vector3.up, transform.rotation.eulerAngles.y);
        projectile.transform.SetParent(gameObject.transform);
    }


}