using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEEEE : MonoBehaviour
{
    private ParticleSystem particleSystem11;

    void Start()
    {
        // ParticleSystem�R���|�[�l���g���擾
        particleSystem11 = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RestartEffect();
        }
    }

    void RestartEffect()
    {
        // �G�t�F�N�g���~���Ă���ēx�Đ�
        particleSystem11.Stop();
        particleSystem11.Clear();  // �p�[�e�B�N�������Z�b�g�i�K�v�ȏꍇ�j
        particleSystem11.Play();
    }
}
