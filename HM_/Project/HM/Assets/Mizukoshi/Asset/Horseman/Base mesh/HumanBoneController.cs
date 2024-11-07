using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBoneController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // ボーンにアニメーションを制御する例
        // 左腕の回転を変更する場合
        HumanPoseHandler poseHandler = new HumanPoseHandler(animator.avatar, animator.transform);
        HumanPose humanPose = new HumanPose();
        poseHandler.GetHumanPose(ref humanPose);

        // 例えば、左腕の回転を手動で変更する
        humanPose.bodyRotation = Quaternion.Euler(0, 90, 0);
        poseHandler.SetHumanPose(ref humanPose);
    }
}
