using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandIK : MonoBehaviour
{
    private Animator p_Animator;

    [SerializeField, Tooltip("LegIKのターゲット")]
    private Transform LeftIKTarget;

    void Start()
    {
        // Animatorの参照を取得する
        p_Animator = GetComponent<Animator>();
        LeftIKTarget = this.gameObject.transform.root.transform.Find("MRTK XR Rig/Camera Offset/MRTK LeftHand Controller");
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
    }
}
