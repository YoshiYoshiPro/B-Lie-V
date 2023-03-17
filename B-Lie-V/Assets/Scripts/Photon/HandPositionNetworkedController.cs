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
            // Transform�̒l���X�g���[���ɏ�������ő��M����
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
        }
        else
        {
            // ��M�����X�g���[����ǂݍ����Transform�̒l���X�V����
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
