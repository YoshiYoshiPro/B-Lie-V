using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ChallengerController : MonoBehaviourPunCallbacks
{

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Jump", RpcTarget.All);
        }
    }

    [PunRPC]
    void Jump()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.up * 250f);
    }

}
