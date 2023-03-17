using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RightHandPUN : MonoBehaviour
{
    private Animator p_Animator;
    private PhotonAnimatorView p_AnimatorView;
    [SerializeField, Tooltip("LegIKのターゲット")]
    private Transform RightIKTarget;
    private PhotonTransformView p_TransformView;

    private PhotonView photonView;

    void Start()
    {
        // Animatorの参照を取得する
        p_Animator = GetComponent<Animator>();
        // PhotonAnimatorViewをアタッチする

        photonView = GetComponent<PhotonView>();
        p_AnimatorView = GetComponent<PhotonAnimatorView>();
        // Transformの同期方法を指定する
        p_TransformView = GetComponent<PhotonTransformView>();

        RightIKTarget = GameObject.Find("MRTK XR Rig/Camera Offset/MRTK RightHand Controller").transform;
    }

    // IPunObservableインターフェースを実装して、同期する情報を指定する
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 左手のIKターゲットの位置と回転を送信する
            stream.SendNext(RightIKTarget.position);
            stream.SendNext(RightIKTarget.rotation);
        }
        else
        {
            // 左手のIKターゲットの位置と回転を受信して、ローカルのIKターゲットを更新する
            RightIKTarget.position = (Vector3)stream.ReceiveNext();
            RightIKTarget.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    // IK更新時に呼ばれる関数
    // IKPassにチェックを入れた場合のみ呼び出される
    void OnAnimatorIK()
    {
        if (RightIKTarget == null) return;

        // 右手のIKを有効化する(重み:1.0)
        p_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        p_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        // 右手のIKのターゲットを設定する
        p_Animator.SetIKPosition(AvatarIKGoal.RightHand, RightIKTarget.position);
        p_Animator.SetIKRotation(AvatarIKGoal.RightHand, RightIKTarget.rotation);


        // PhotonAnimatorViewにIKの状態を送信する
        if (photonView.IsMine)
        {
            p_AnimatorView.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Continuous);
            p_AnimatorView.SetParameterSynchronized("IKWeight", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
            p_AnimatorView.SetParameterSynchronized("RightHandPos", PhotonAnimatorView.ParameterType.Vector3, PhotonAnimatorView.SynchronizeType.Continuous);
            p_AnimatorView.SetParameterSynchronized("RightHandRot", PhotonAnimatorView.ParameterType.Quaternion, PhotonAnimatorView.SynchronizeType.Continuous);
            p_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, p_Animator.GetIKPositionWeight(AvatarIKGoal.RightHand));
            p_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, p_Animator.GetIKRotationWeight(AvatarIKGoal.RightHand));
            p_Animator.SetIKPosition(AvatarIKGoal.RightHand, RightIKTarget.position);
            p_Animator.SetIKRotation(AvatarIKGoal.RightHand, RightIKTarget.rotation);
        }
    }
}
