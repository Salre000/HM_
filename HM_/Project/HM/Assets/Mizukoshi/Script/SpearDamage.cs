using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour
{

    public int damege = 10;

    // �^�O�̖��O
    private string _tagName = "EnemyAttack";

    // �������Q�[���I�u�W�F�N�g�̐���
    private GameObject _gameObject;


    // Start is called before the first frame update
    void Start()
    {
        // �^�O�̂��Ă���I�u�W�F�N�g��T��
        _gameObject=GameObject.FindGameObjectWithTag( _tagName );

        // �_���[�W���Z�b�g����
        _gameObject.GetComponent<Damage>().SetDamage( damege );

        Debug.Log(_gameObject.GetComponent<Damage>().GetDamage().ToString());
    }
}
