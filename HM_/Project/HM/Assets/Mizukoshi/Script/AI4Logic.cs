using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���̍s���_����\���N���X    �s���̊�{
/// </summary>
public class AI4Logic : Hunter_AI
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �������Ă��邩�݂����Ă��Ȃ���
        if (!monsterDisplay)
        {
            // �S����Ԃ��ǂ���
            if (CheckRest())
            {
                // �������Ȃ�
                return;
            }
            else
            {
                // ���񂷂�
                Search();
            }
        }
        else
        {
            // �S����Ԃ��ǂ���
            if (CheckRest())
            {
                // �������Ȃ�
                return ;
            }
            else
            {
                // �������������ǂ���
                if (CheckAttackDistance(this.gameObject))
                {
                    // �߂��Ɍ�����
                    Chase();
                }
                else
                {
                    // ��������Ԃł����
                    if (attackReady)
                    {
                        // �������
                        Avoid();
                    }
                    else
                    {
                        // �U���̃N�[���^�C�������邩�ǂ������m�F����
                        if (attackReady)
                        {
                            // �U������
                            Attack();
                        }
                        else
                        {
                           
                            // ��ނ���
                            Back();
                        }
                    }
                }
            }
            
        }
    }
}
