using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()//todo 2 oyuncu bağlandığında hangi scriptlerin kapatılması gerektiğine bak.
    {
        if (photonView.IsMine)
        {
            gameObject.GetComponent<RecievedMovement>().enabled = true;

        }
        else
        {
            gameObject.GetComponent<RecievedMovement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
