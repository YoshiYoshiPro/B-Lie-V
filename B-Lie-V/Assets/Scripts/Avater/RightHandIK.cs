using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandIK : MonoBehaviour
{
    private Animator p_Animator;

    [SerializeField, Tooltip("LegIK�̃^�[�Q�b�g")]
    private Transform RightIKTarget;

    void Start()
    {
        // Animator�̎Q�Ƃ��擾����
        p_Animator = GetComponent<Animator>();

        RightIKTarget = this.gameObject.transform.root.transform.Find("MRTK XR Rig/Camera Offset/MRTK RightHand Controller");
    }

    void Update()
    {

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
    }
}
