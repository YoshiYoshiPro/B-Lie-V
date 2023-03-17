using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RightHandPUN : MonoBehaviour
{
    private Animator p_Animator;
    private PhotonAnimatorView p_AnimatorView;
    [SerializeField, Tooltip("LegIK�̃^�[�Q�b�g")]
    private Transform RightIKTarget;
    private PhotonTransformView p_TransformView;

    private PhotonView photonView;

    void Start()
    {
        // Animator�̎Q�Ƃ��擾����
        p_Animator = GetComponent<Animator>();
        // PhotonAnimatorView���A�^�b�`����

        photonView = GetComponent<PhotonView>();
        p_AnimatorView = GetComponent<PhotonAnimatorView>();
        // Transform�̓������@���w�肷��
        p_TransformView = GetComponent<PhotonTransformView>();

        RightIKTarget = GameObject.Find("MRTK XR Rig/Camera Offset/MRTK RightHand Controller").transform;
    }

    // IPunObservable�C���^�[�t�F�[�X���������āA������������w�肷��
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // �����IK�^�[�Q�b�g�̈ʒu�Ɖ�]�𑗐M����
            stream.SendNext(RightIKTarget.position);
            stream.SendNext(RightIKTarget.rotation);
        }
        else
        {
            // �����IK�^�[�Q�b�g�̈ʒu�Ɖ�]����M���āA���[�J����IK�^�[�Q�b�g���X�V����
            RightIKTarget.position = (Vector3)stream.ReceiveNext();
            RightIKTarget.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    // IK�X�V���ɌĂ΂��֐�
    // IKPass�Ƀ`�F�b�N����ꂽ�ꍇ�̂݌Ăяo�����
    void OnAnimatorIK()
    {
        if (RightIKTarget == null) return;

        // �E���IK��L��������(�d��:1.0)
        p_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        p_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        // �E���IK�̃^�[�Q�b�g��ݒ肷��
        p_Animator.SetIKPosition(AvatarIKGoal.RightHand, RightIKTarget.position);
        p_Animator.SetIKRotation(AvatarIKGoal.RightHand, RightIKTarget.rotation);


        // PhotonAnimatorView��IK�̏�Ԃ𑗐M����
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
