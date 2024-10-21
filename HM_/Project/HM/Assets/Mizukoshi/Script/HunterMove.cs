using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterMove : MonoBehaviour
{
    public Transform goal;          //�ړI�n�ƂȂ�I�u�W�F�N�g�̃g�����X�t�H�[���i�[�p
    private NavMeshAgent agent;     //�G�[�W�F���g�ƂȂ�I�u�W�F�N�g��NavMeshAgent�i�[�p 

    // Use this for initialization
    void Start()
    {
        //�G�[�W�F���g��NaveMeshAgent���擾����
        agent = GetComponent<NavMeshAgent>();

        //�ړI�n�ƂȂ���W��ݒ肷��
        agent.destination = goal.position;
    }
}