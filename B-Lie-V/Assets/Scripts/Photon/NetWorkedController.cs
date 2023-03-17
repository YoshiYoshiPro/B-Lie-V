using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class NetWorkedController : MonoBehaviourPunCallbacks, IPunObservable
{
    Transform cameraTransform;

    //public TextMeshProUGUI text;
    [SerializeField] GameObject avater_Face;
    [SerializeField] GameObject avater_RightHand;
    private Transform rightHandTransform;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Transformの値をストリームに書き込んで送信する
            stream.SendNext(transform.position);
            //Debug.Log("stream send : " + transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
        }
        else
        {
            // 受信したストリームを読み込んでTransformの値を更新する
            transform.position = (Vector3)stream.ReceiveNext();
            //Debug.Log("Receive : " + transform.position);
            transform.rotation = (Quaternion)stream.ReceiveNext();
            transform.localScale = (Vector3)stream.ReceiveNext();
        }
    }

    void Start()
    {
        //cameraTransform = GameObject.Find("Player/MRTK XR Rig/Camera Offset").transform;
        cameraTransform = Camera.main.GetComponent<Transform>();
        rightHandTransform = GameObject.Find("Player/MRTK XR Rig/Camera Offset/MRTK RightHand Controller").transform;
        if (photonView.IsMine)
        {
            avater_Face.transform.parent = cameraTransform;
            //text.text = avater_Face.name;
            avater_Face.transform.localPosition = Vector3.zero;
            avater_Face.transform.localRotation = Quaternion.identity;
            avater_RightHand.transform.parent = rightHandTransform;

            avater_RightHand.transform.localPosition = Vector3.zero;
            avater_RightHand.transform.localRotation = Quaternion.identity;

        }
    }
}
