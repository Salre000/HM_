using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour
{

    public int damege = 10;

    // タグの名前
    private string _tagName = "EnemyAttack";

    // やりをもつゲームオブジェクトの生成
    private GameObject _gameObject;


    // Start is called before the first frame update
    void Start()
    {
        // タグのついているオブジェクトを探し
        _gameObject=GameObject.FindGameObjectWithTag( _tagName );

        // ダメージをセットする
        _gameObject.GetComponent<Damage>().SetDamage( damege );

        Debug.Log(_gameObject.GetComponent<Damage>().GetDamage().ToString());
    }
}
