using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandIK : MonoBehaviour
{
    private Animator p_Animator;

    [SerializeField, Tooltip("LegIKのターゲット")]
    private Transform RightIKTarget;

    void Start()
    {
        // Animatorの参照を取得する
        p_Animator = GetComponent<Animator>();

        RightIKTarget = this.gameObject.transform.root.transform.Find("MRTK XR Rig/Camera Offset/MRTK RightHand Controller");
    }

    void Update()
    {

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
    }
}
