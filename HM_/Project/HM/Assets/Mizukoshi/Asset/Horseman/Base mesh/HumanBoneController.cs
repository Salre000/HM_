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
        // �{�[���ɃA�j���[�V�����𐧌䂷���
        // ���r�̉�]��ύX����ꍇ
        HumanPoseHandler poseHandler = new HumanPoseHandler(animator.avatar, animator.transform);
        HumanPose humanPose = new HumanPose();
        poseHandler.GetHumanPose(ref humanPose);

        // �Ⴆ�΁A���r�̉�]���蓮�ŕύX����
        humanPose.bodyRotation = Quaternion.Euler(0, 90, 0);
        poseHandler.SetHumanPose(ref humanPose);
    }
}
