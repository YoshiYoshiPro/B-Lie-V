using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HandPositionNetworkedController : MonoBehaviourPunCallbacks, IPunObservable
{
    Transform HandTransform;
    [SerializeField] GameObject HandPosition;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Transformの値をストリームに書き込んで送信する
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
        }
        else
        {
            // 受信したストリームを読み込んでTransformの値を更新する
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            transform.localScale = (Vector3)stream.ReceiveNext();
        }
    }

    void Start()
    {

        HandTransform = GameObject.Find("Player/MRTK XR Rig/Camera Offset/MRTK LeftHand Controller").transform;

        if (photonView.IsMine)
        {
            HandPosition.transform.parent = HandTransform;
            HandPosition.transform.localPosition = Vector3.zero;
            HandPosition.transform.localRotation = Quaternion.identity;
        }
    }
}
