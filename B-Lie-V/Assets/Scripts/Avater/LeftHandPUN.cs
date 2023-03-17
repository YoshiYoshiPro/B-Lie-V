using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LeftHandPUN : MonoBehaviour
{
    private Animator p_Animator;
    private PhotonAnimatorView p_AnimatorView;
    [SerializeField, Tooltip("LegIKのターゲット")]
    private Transform LeftIKTarget;
    private PhotonTransformView p_TransformView;

    private PhotonView photonView;

    void Start()
    {
        // Animatorの参照を取得する
        p_Animator = GetComponent<Animator>();

        photonView = GetComponent<PhotonView>();
        // PhotonAnimatorViewをアタッチする
        p_AnimatorView = GetComponent<PhotonAnimatorView>();
        // Transformの同期方法を指定する
        p_TransformView = GetComponent<PhotonTransformView>();
        if (photonView.IsMine)
        {
            LeftIKTarget = GameObject.Find("MRTK XR Rig/Camera Offset/MRTK LeftHand Controller/LeftHand").transform;
        }
        
    }

    // IPunObservableインターフェースを実装して、同期する情報を指定する
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 左手のIKターゲットの位置と回転を送信する
            stream.SendNext(LeftIKTarget.position);
            stream.SendNext(LeftIKTarget.rotation);
        }
        else
        {
            // 左手のIKターゲットの位置と回転を受信して、ローカルのIKターゲットを更新する
            LeftIKTarget.position = (Vector3)stream.ReceiveNext();
            LeftIKTarget.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update()
    {

    }

    // IK更新時に呼ばれる関数
    // IKPassにチェックを入れた場合のみ呼び出される
    void OnAnimatorIK()
    {
        if (LeftIKTarget == null) return;

        // 左手のIKを有効化する(重み:1.0)
        p_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        p_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        // 左手のIKのターゲットを設定する
        p_Animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftIKTarget.position);
        p_Animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftIKTarget.rotation);

        // PhotonAnimatorViewにIKの状態を送信する
        if (photonView.IsMine)
        {
            p_AnimatorView.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Continuous);
            p_AnimatorView.SetParameterSynchronized("IKWeight", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
            p_AnimatorView.SetParameterSynchronized("LeftHandPos", PhotonAnimatorView.ParameterType.Vector3, PhotonAnimatorView.SynchronizeType.Continuous);
            p_AnimatorView.SetParameterSynchronized("LeftHandRot", PhotonAnimatorView.ParameterType.Quaternion, PhotonAnimatorView.SynchronizeType.Continuous);
            p_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, p_Animator.GetIKPositionWeight(AvatarIKGoal.LeftHand));
            p_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, p_Animator.GetIKRotationWeight(AvatarIKGoal.LeftHand));
            p_Animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftIKTarget.position);
            p_Animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftIKTarget.rotation);
        }
    }
}
