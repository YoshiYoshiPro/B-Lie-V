using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LeftHandPUN : MonoBehaviour
{
    private Animator p_Animator;
    private PhotonAnimatorView p_AnimatorView;
    [SerializeField, Tooltip("LegIK�̃^�[�Q�b�g")]
    private Transform LeftIKTarget;
    private PhotonTransformView p_TransformView;

    private PhotonView photonView;

    void Start()
    {
        // Animator�̎Q�Ƃ��擾����
        p_Animator = GetComponent<Animator>();

        photonView = GetComponent<PhotonView>();
        // PhotonAnimatorView���A�^�b�`����
        p_AnimatorView = GetComponent<PhotonAnimatorView>();
        // Transform�̓������@���w�肷��
        p_TransformView = GetComponent<PhotonTransformView>();
        if (photonView.IsMine)
        {
            LeftIKTarget = GameObject.Find("MRTK XR Rig/Camera Offset/MRTK LeftHand Controller/LeftHand").transform;
        }
        
    }

    // IPunObservable�C���^�[�t�F�[�X���������āA������������w�肷��
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // �����IK�^�[�Q�b�g�̈ʒu�Ɖ�]�𑗐M����
            stream.SendNext(LeftIKTarget.position);
            stream.SendNext(LeftIKTarget.rotation);
        }
        else
        {
            // �����IK�^�[�Q�b�g�̈ʒu�Ɖ�]����M���āA���[�J����IK�^�[�Q�b�g���X�V����
            LeftIKTarget.position = (Vector3)stream.ReceiveNext();
            LeftIKTarget.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update()
    {

    }

    // IK�X�V���ɌĂ΂��֐�
    // IKPass�Ƀ`�F�b�N����ꂽ�ꍇ�̂݌Ăяo�����
    void OnAnimatorIK()
    {
        if (LeftIKTarget == null) return;

        // �����IK��L��������(�d��:1.0)
        p_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        p_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        // �����IK�̃^�[�Q�b�g��ݒ肷��
        p_Animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftIKTarget.position);
        p_Animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftIKTarget.rotation);

        // PhotonAnimatorView��IK�̏�Ԃ𑗐M����
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
