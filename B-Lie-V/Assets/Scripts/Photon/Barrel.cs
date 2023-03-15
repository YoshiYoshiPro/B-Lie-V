using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrel : MonoBehaviourPunCallbacks, IPunObservable
{

    private Vector3 networkPosition;
    private Quaternion networkRotation;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            Debug.Log("stream send : " + transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            Debug.Log("Receive : " + transform.position);
        }
    }
}
