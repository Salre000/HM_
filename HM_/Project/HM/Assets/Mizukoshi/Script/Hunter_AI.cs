using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter_AI : MonoBehaviour
{
    // �@ ������30�ȏ゠��Ȃ�΃i�r���b�V���ɂ��ړ�

    // �A ������30�ȉ��Ȃ�΂������ړ�

    // �B �G���U����5�񂵂Ă�����܂��̗͑͂�20�ȉ��Ȃ��x�����

    // �C ������10�ȉ��Ȃ�΍U������B

    float distance = 0;
    int attackNum = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �U����5��ڂɂȂ����炢������悯��
        if (attackNum % 5 == 0)
        {
            // ������
        }
        else
        {

            if (distance >= 30)
            {
                // �i�r���b�V���ɂ��ړ�
            }
            else if (distance < 30 && distance > 10)
            {
                // �������ړ�
            }

            else if (distance < 10)
            {
                // �U��

            }

        }

    }
}
