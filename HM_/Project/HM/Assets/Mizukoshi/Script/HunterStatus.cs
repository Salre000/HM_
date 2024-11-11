using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterStatus : MonoBehaviour
{
    private GameObject monster;

    private GameObject[] ketBorn = new GameObject[4];

    LayerMask layerMask;

    //114   //23     57      54

    // s“®‚ÌŠî€‚É‚È‚éó‘Ô
    public enum MoveStatus
    {
        // —§‚¿s‚­‚·
        None,

        Search,

        // Œx‰ú
        Vigilance,

        Move,

        Attack,

        Back,
    }
    public MoveStatus moveStatus;

    // ó‘ÔˆÙí‚ğw‚·‚à‚Ì
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


    public void IsEnemyVisibleFront(Vector3 AIposition,float height=2.0f,float radius=0.5f)
    {
        Ray[] betweenRay=new Ray[4];
        bool []noneObstacle=new bool[4];
        for (int i = 0; i < ketBorn.Length; i++)
        {
            betweenRay[i]= new Ray(AIposition + height * Vector3.up, ketBorn[i].transform.position.normalized);
            if (Physics.SphereCast(betweenRay[i], radius, (ketBorn[i].transform.position - AIposition).magnitude, layerMask))
            {
                // áŠQ•¨‚ª‚È‚¢‚Æ”»’è
                noneObstacle[i] = false;
            }
            else
            {
                noneObstacle[i] = true;
            }
        }
        
    }
}
