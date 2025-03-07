using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterStatus : MonoBehaviour
{
    private GameObject monster;

    private GameObject[] ketBorn = new GameObject[4];

    LayerMask layerMask;

    //114   //23     57      54

    // 行動の基準になる状態
    public enum MoveStatus
    {
        // 立ち尽くす
        None,

        Search,

        // 警戒
        Vigilance,

        Move,

        Attack,

        Back,
    }
    public MoveStatus moveStatus;

    // 状態異常を指すもの
    public enum AbnormalStatus
    {
        None,
        Stan,
    }

    public AbnormalStatus abnormalStatus;

    // Start is called before the first frame update
    void Start()
    {
        abnormalStatus=AbnormalStatus.None;
        moveStatus=MoveStatus.None;
        monster = GameObject.FindGameObjectWithTag("Player");
        ketBorn[0] = GameObject.Find("Bone.114");
        ketBorn[1]= GameObject.Find("Bone.023");
        ketBorn[2]= GameObject.Find("Bone.057");
        ketBorn[3]= GameObject.Find("Bone.054");
        layerMask = LayerMask.GetMask("Terrain");
    }

    public void Update()
    {
        IsEnemyVisibleFront(this.transform.position);
    }


    public void IsEnemyVisibleFront(Vector3 AIposition,float height=2.0f,float radius=0.5f)
    {
        Ray[] betweenRay=new Ray[4];
        bool []noneObstacle=new bool[4];
        
        for (int i = 0; i < ketBorn.Length; i++)
        {
            betweenRay[i]= new Ray(AIposition + height * Vector3.up, ketBorn[i].transform.position.normalized);
            Debug.DrawRay(AIposition + height * Vector3.up, ketBorn[i].transform.position.normalized,Color.red);
            if (Physics.SphereCast(betweenRay[i], radius, (ketBorn[i].transform.position - AIposition).magnitude, layerMask))
            {
                // 障害物がないと判定
                noneObstacle[i] = false;
            }
            else
            {
                noneObstacle[i] = true;
            }
        }
        
    }





}
