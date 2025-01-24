using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavObstacle : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;
    /// <summary>
    /// �i�r���b�V����obstacl�R���|�[�l���g���A�^�b�`����֐�
    /// </summary>
    public void AttachNavmeshObstacle(GameObject targetObject)
    {
        // NavMeshObstacle�R���|�[�l���g�����ɃA�^�b�`����Ă��Ȃ��ꍇ�̂ݒǉ�
        if (GetComponent<NavMeshObstacle>() == null)
        {
            // NavMeshObstacle���I�u�W�F�N�g�ɒǉ�
            NavMeshObstacle navMeshObstacle = targetObject.AddComponent<NavMeshObstacle>();

            // �K�v�ɉ����Đݒ��ύX
            navMeshObstacle.carving = true;
            navMeshObstacle.shape = NavMeshObstacleShape.Capsule;
            navMeshObstacle.radius = 0.5f;
            navMeshObstacle.height = 3.0f;
            navMeshObstacle.center = new Vector3(0, 0, 0);
        }
    }
}
