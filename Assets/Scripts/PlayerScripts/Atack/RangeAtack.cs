using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAtack : MonoBehaviour
{

    //private CreepAI creepAIScript; creplerin en yakýndaki hedefe saldýrdýðý script
    GameObject projectile;

    private Vector3 projectilePos = new Vector3(-0.15f, 1, 2.15f); 


    //todo start fonksiyonunda swtich case gibi bir yapýyla üretilecek range atacklarýn prefablerini hero adýna crep adýna göre eþleþtir.
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