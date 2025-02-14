using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemySight : MonoBehaviour
{
    public float viewAngle = 60f; // ����p�x�i�~���̊p�x�j
    public float viewDistance = 10f; // ����̋���
    public Transform player; // �v���C���[��Transform

    void Update()
    {
        OnDrawGizmos();
        if (IsPlayerInSight())
        {
            Debug.Log("Player detected!");
        }
    }

    // ������Ƀv���C���[�����邩�`�F�b�N����֐�
    bool IsPlayerInSight()
    {
        Vector3 toPlayer = player.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;

        // �v���C���[������͈͓̔����A���������e����Ă��邩�`�F�b�N
        if (distanceToPlayer <= viewDistance)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, toPlayer);

            // �v���C���[������p�x���ɂ��邩�ǂ���
            if (angleToPlayer <= viewAngle / 2f)
            {
                // �v���C���[��������ɂ���ꍇ�́A�����Raycast�ŏ�Q�����Ȃ������`�F�b�N
                RaycastHit hit;
                if (Physics.Raycast(transform.position, toPlayer.normalized, out hit, viewDistance))
                {
                    if (hit.transform == player)
                    {
                        return true; // �v���C���[�����E���ɂ���
                    }
                }
            }
        }

        return false; // �v���C���[�����E�O�ɂ���
    }
    void OnDrawGizmos()
    {
        // ����͈̔͂��~���Ƃ��ĕ`��
        Gizmos.color = Color.green;
        Vector3 forward = transform.forward * viewDistance;
        Vector3 left = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2f, 0) * forward;

        Gizmos.DrawLine(transform.position, transform.position + left);
        Gizmos.DrawLine(transform.position, transform.position + right);
        Gizmos.DrawLine(transform.position + left, transform.position + right);
    }
}
