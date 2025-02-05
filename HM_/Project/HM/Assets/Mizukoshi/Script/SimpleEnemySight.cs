using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySight : MonoBehaviour
{
    public float sightRange = 10f; // ���F����
    public float fieldOfViewAngle = 110f; // ���E�p�x
    public LayerMask playerLayer; // �v���C���[�̃��C���[

    private Transform player; // �v���C���[��Transform



    void Start()
    {
        // �v���C���[��Transform���擾
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // ���E�����ǂ������`�F�b�N
        if (IsPlayerInSight())
        {
            Debug.Log("�v���C���[�𔭌��I");
        }
    }

    bool IsPlayerInSight()
    {
        // �v���C���[�Ƃ̕����x�N�g�����v�Z
        Vector3 directionToPlayer = player.position - transform.position;

        // �v���C���[�����E�p�x���ɂ��邩���`�F�b�N
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        if (angleToPlayer < fieldOfViewAngle / 2)
        {
            // �v���C���[�����E�p�x���ɂ���ꍇ�A���C���΂��ĎՕ������Ȃ����m�F
            float distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer <= sightRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, sightRange, playerLayer))
                {
                    // ���C���v���C���[�ɓ��������ꍇ�A���E���Ƀv���C���[������

                    return true;
                }
            }
        }
        return false;
    }
}
