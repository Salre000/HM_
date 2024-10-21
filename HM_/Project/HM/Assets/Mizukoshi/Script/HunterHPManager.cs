using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // ハンターのHP
    public int hp = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        // タグの名前によってダメージ処理を行う
        if (collision.gameObject.tag == "Player")
        {
            Damage(10/**/);
        }
    }

    // HPの減少処理 
    void Damage(int damage)
    {
        hp -= damage;
        // HPが0になったら
        if (hp < 0)
        {
            hp = 0;
        }
    }
}
