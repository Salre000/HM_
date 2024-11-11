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
    private GameObject _attackObject;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        _anime = GetComponent<PlayerAnime>();
        attackPosition = GameObject.Find("Bone.024");

        Application.targetFrameRate = 60;
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

        //‰“‹——£UŒ‚‚ð‚·‚éƒ{ƒ^ƒ“
        if (Input.GetKey("joystick button 6")) 
        {
            _anime.SetLoanAttackFlag(true);


        }
        else 
        {
            _anime.SetLoanAttackFlag(false);

        }
        //UŒ‚‚ð‚·‚éƒ{ƒ^ƒ“
        if (Input.GetKey("joystick button 7")) 
        {
            _anime.SetAttackFlag(true);


        }
        else
        {
            _anime.SetAttackFlag(false);

        }

        //™ôšK
        if(Input.GetKey("joystick button 4")&& Input.GetKey("joystick button 5"))
        {
            _anime.SetRoarFlag(true);
        }
        else
        {
            _anime.SetRoarFlag(false);
        }

        if (Input.GetAxis("Vertical")>=-0.3f && Input.GetKey("joystick button 3") || Input.GetKey("joystick button 2")) 
        {
            _anime.SetJumpFlag(true);


        }
        else 
        {
            _anime.SetJumpFlag(false);

        }
        if (Input.GetAxis("Vertical")<-0.3f&& Input.GetKey("joystick button 3")|| Input.GetKey("joystick button 2")) 
        {
            _anime.SetBackSteppeFlag(true);


        }
        else 
        {
            _anime.SetBackSteppeFlag(false);


        }

    }


    void ResetObject() 
    {

    }
}
