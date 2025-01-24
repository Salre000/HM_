using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ��Q����ݒ�@�@
/// ��1      㩐������_�Ŋm��
/// ��2      ��藐���������Ԃ�Obstacle�R���|�[�l���g��OnOff����
/// ��3�@�@�@���͂�Navmesh�����������A�Ǝ��̍s�����s�킹��
/// ��4      㩐�����AI���_����݂Ď��F�p�x�����F��������薢���Ŏ��Ԍv���J�n
/// �@�@�@�@ ��莞�Ԍo�߂ł�����Obstacle�R���|�[�l���g������B
/// �@�@�@�@ ���F�p�x�A���F����
/// </summary>
public class TestObstacle : MonoBehaviour
{

    private NavMeshObstacle navMeshObstacle;
    /// <summary>
    /// �i�r���b�V����obstacl�R���|�[�l���g���A�^�b�`����֐�
    /// </summary>
    public void AttachNavmeshObstacle()
    {
        // NavMeshObstacle�R���|�[�l���g�����ɃA�^�b�`����Ă��Ȃ��ꍇ�̂ݒǉ�
        if (GetComponent<NavMeshObstacle>() == null)
        {
            // NavMeshObstacle���I�u�W�F�N�g�ɒǉ�
            NavMeshObstacle navMeshObstacle = gameObject.AddComponent<NavMeshObstacle>();

            // �K�v�ɉ����Đݒ��ύX
            navMeshObstacle.carving = true; 
            navMeshObstacle.shape = NavMeshObstacleShape.Capsule;
            navMeshObstacle.radius = 0.5f; 
            navMeshObstacle.height = 3.0f; 
            navMeshObstacle.center = new Vector3(0,0,0);    
        }
    }
}
