using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���̍s���_����\���N���X    �s���̊�{
/// </summary>
public class AI4Logic : Hunter_AI
{
    
    public bool disappear=false;

    public bool notMoveActive=false;

    public bool attackDistance=false;

    public bool avoidActive=false;

    public bool readyAttack=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �������Ă��邩�݂����Ă��Ȃ���
        if (!disappear)
        {
            // �S����Ԃ��ǂ���
            if (notMoveActive)
            {
                // �������Ȃ�
            }
            else
            {
                // ���񂷂�
                
            }
        }
        else
        {
            // �S����Ԃ��ǂ���
            if (notMoveActive)
            {
                // �������Ȃ�
            }
            else
            {
                // �������������ǂ���
                if (!attackDistance)
                {
                    // �߂��Ɍ�����
                    Chase();
                }
                else
                {
                    // ��������Ԃł����
                    if (avoidActive)
                    {
                        // �������
                        Avoid();
                    }
                    else
                    {
                        // �U���̃N�[���^�C�������邩�ǂ������m�F����
                        if (readyAttack)
                        {
                            // �U������
                            Attack();
                        }
                        else
                        {
                            // �N�[���^�C����҂�
                        }
                    }
                }
            }
            
        }
    }
}
