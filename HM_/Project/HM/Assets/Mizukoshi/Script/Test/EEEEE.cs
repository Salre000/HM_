using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEEEE : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        // ParticleSystem�R���|�[�l���g���擾
        particleSystem = GetComponent<ParticleSystem>();
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
        particleSystem.Stop();
        particleSystem.Clear();  // �p�[�e�B�N�������Z�b�g�i�K�v�ȏꍇ�j
        particleSystem.Play();
    }
}
