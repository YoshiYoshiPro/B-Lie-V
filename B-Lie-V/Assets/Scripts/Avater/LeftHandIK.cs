using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandIK : MonoBehaviour
{
    private Animator p_Animator;

    [SerializeField, Tooltip("LegIK�̃^�[�Q�b�g")]
    private Transform LeftIKTarget;

    void Start()
    {
        // Animator�̎Q�Ƃ��擾����
        p_Animator = GetComponent<Animator>();
        LeftIKTarget = this.gameObject.transform.root.transform.Find("MRTK XR Rig/Camera Offset/MRTK LeftHand Controller");
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
    }
}
