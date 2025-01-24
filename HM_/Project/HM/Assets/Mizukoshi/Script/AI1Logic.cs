using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// メイスの行動論理のクラス
/// </summary>
public class AI1Logic : MonoBehaviour
{
    private GameObject targetObject;
    private NavMeshAgent agent;

    private Vector3 destination;

    public float keepDistance = 2.0f;

    int avoidRatio = 7;

    // 
    public float offsetX = 0;
    public float offsetY = 0;
    public float offsetZ = 2.0f;

    private void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            agent.destination=GetDestinationPosition();
        }

        // プレイヤーの方向に向く
        this.transform.LookAt(targetObject.transform.position);

        
        // 回避行動関数
    }

    // 目的地の取得
    Vector3 GetDestinationPosition()
    {
        Vector3 newPos=targetObject.transform.position;
        Vector3 offset=new Vector3(offsetX, offsetY, offsetZ);
        offset=targetObject.transform.rotation*offset;
        newPos=newPos+offset;
        return newPos;
    }

    void Avoid()
    {
        int random = Random.Range(0, 10);
        if (random <= avoidRatio)
        {
            // 回避アニメーション関数
            Debug.Log("Avoid");
        }
        
    }


}
