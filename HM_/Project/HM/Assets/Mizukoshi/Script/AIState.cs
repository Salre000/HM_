using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
   // ��ԊǗ�
   public enum State
   {
        // �ҋ@
        Idle,

        // �T��
        Search,

        // �ǐ�
        Chase,

        // �U��
        Attack,

        // �������
        Evade,
   }
    public State state;
}
