using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTrap : MonoBehaviour
{
    GameObject Hunter;
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Hunter") return;

        if (Hunter != null) return;
        Hunter=other.gameObject;

        //�S���̊֐����Ă�


    }

    bool Flag = false;  
    public void SetStart() { Flag = true; }

    float time = 0;
    
    private void FixedUpdate()
    {
        if (!Flag) return;
        time += Time.deltaTime;

        if (time < 4) return;
        
        Flag = false;


        //�n���^�[�̍S���U�����I�����鏈��������

        Hunter = null;

    }

}
