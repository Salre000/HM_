using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NotEnemySight3 : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public float searchRadius = 10f;
    private int currentWaypoint = 0;

    private NotEnemySight2 headMovement; // ��̓����𐧌䂷��X�N���v�g

    void Start()
    {
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }

        // HeadMovement�X�N���v�g�̎Q�Ƃ��擾
        headMovement = GetComponent<NotEnemySight2>();
    }

    void Update()
    {
        // �ړI�n�ɓ��B�����玟�̃E�F�C�|�C���g�Ɉړ�
        if (Vector3.Distance(transform.position, agent.destination) < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }

        // ���G�Ɗ������낫��낳����
        //SearchForPlayer();

        // ��̓������X�V
        headMovement.Update();
    }

    void SearchForPlayer()
    {
        // �v���C���[�����o����͈͂�ݒ�
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("Player detected!");
                break;
            }
        }
    }
}
