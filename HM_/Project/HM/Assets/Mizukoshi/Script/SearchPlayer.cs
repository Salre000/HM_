using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    public float detectionRange = 1000f;  // ���G�͈�
    public float detectionAngle = 60f;  // ���E�p�x�i�Ⴆ��60�x�ȓ��̃v���C���[�����o�j
    public Transform player;            // �v���C���[��Transform

    void Update()
    {
        // �v���C���[�Ƃ̋������擾
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        

        if (distanceToPlayer <= detectionRange)
        {
            // ���E���Ƀv���C���[�����邩����i���E�p�x���l���j
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

          

            if (angle <= detectionAngle / 2)
            {
                Debug.Log("Player detected!");
            }
        }
    }
}
