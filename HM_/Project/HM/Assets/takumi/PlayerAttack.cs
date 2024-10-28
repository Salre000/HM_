using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnime _anime;
    GameObject attackPosition;
    Rigidbody rb;
    private GameObject _attackObject;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        _anime = GetComponent<PlayerAnime>();
        attackPosition = GameObject.Find("Bone.024");

        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
    }

    [SerializeField]private float _counter = 0;
    [SerializeField] private float _maxCount = 5;
    private bool flag = false;
    // Update is called once per frame
    void Update()
    {

        if (flag) 
        {

            _counter += 1;

            if (_counter > _maxCount) 
            {
                _counter = 0;
                flag = false;
             //this.transform.position = position;

            }

        }

        if (_attackObject != null) 
        {

            _attackObject.transform.position=attackPosition.transform.position;


        }

        //çUåÇÇÇ∑ÇÈÉ{É^Éì
        if (Input.GetKeyUp("joystick button 7")) 
        {

            _anime.SetAttackFlag(true);
            Debug.Log("7");

            if(_attackObject == null) 
            {

                _attackObject = new GameObject();

                _attackObject.tag = "EnemyAttack";

                Damage damage=_attackObject.AddComponent<Damage>();

                damage.SetDamage(0);

                SphereCollider sphere= _attackObject.AddComponent<SphereCollider>();

                sphere.center = Vector3.zero;

                sphere.radius = 1.0f;
                
                position=this.transform.position;

            }
           


        }



    }


    void ResetObject() 
    {
        Destroy(_attackObject);
        _attackObject = null;
        flag = true;
    }
}
