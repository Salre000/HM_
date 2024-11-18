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

    [SerializeField] private Tag TagBox;
    public Tag GetTag() { return TagBox; }
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

    public float V = 0;
    // Update is called once per frame
    void Update()
    {
        V = Input.GetAxis("Vertical");

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

        //遠距離攻撃をするボタン
        if (Input.GetKey("joystick button 6")) 
        {
            _anime.SetLoanAttackFlag(true);


        }
        else 
        {
            _anime.SetLoanAttackFlag(false);

        }
        //攻撃をするボタン
        if (Input.GetKey("joystick button 7")) 
        {
            _anime.SetAttackFlag(true);

            AnimeAttackNormal AttackNormalAnime = this.gameObject.GetComponent<AnimeAttackNormal>();

            if (AttackNormalAnime == null)
            {
                AttackNormalAnime = this.gameObject.AddComponent<AnimeAttackNormal>();

                AttackNormalAnime.TagBox = TagBox;

            }

        }
        else
        {
            _anime.SetAttackFlag(false);

        }

        //咆哮
        if(Input.GetKey("joystick button 4")&& Input.GetKey("joystick button 5"))
        {
            _anime.SetRoarFlag(true);
        }
        else
        {
            _anime.SetRoarFlag(false);
        }

        //前ジャンプ
        if (Input.GetAxis("Vertical")>=-0.3f &&Input.GetKey("joystick button 2")) 
        {
            _anime.SetJumpFlag(true);

            JumpAnime jumpAnime=this.gameObject.GetComponent<JumpAnime>();

            if (jumpAnime == null)
            {
                jumpAnime=this.gameObject.AddComponent<JumpAnime>();

                jumpAnime.TagBox = TagBox;

            }


        }
        else 
        {
            _anime.SetJumpFlag(false);

        }

        //バックジャンプ
        if (Input.GetAxis("Vertical")<-0.3f&&Input.GetKey("joystick button 2")) 
        {
            _anime.SetBackSteppeFlag(true);

            BackSteppeAnime backSteppeAnime = this.gameObject.GetComponent<BackSteppeAnime>();

            if (backSteppeAnime == null)
            {
                backSteppeAnime = this.gameObject.AddComponent<BackSteppeAnime>();

            }



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
