using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRaY : MonoBehaviour
{
    public GameObject person1; 
    public GameObject person2; 
    public GameObject block;   



    void Update()
    {
        // Rayを飛ばす：人1から人2に向けて
        Ray ray = new Ray(person1.transform.position, person2.transform.position - person1.transform.position);
        RaycastHit hit;

        // Rayが何かに衝突したか判定
        if (Physics.Raycast(ray, out hit))
        {
            // 衝突したオブジェクトがブロックだった場合
            if (hit.collider.gameObject == block)
            {
                Debug.Log("1から2はブロックによって遮られています。");
            }
            else
            {
                Debug.Log("1から2は見えます。");
            }
        }
        else
        {
            Debug.Log("1から2は見えます。");
        }

        // Rayの可視化（デバッグ用）
        Debug.DrawRay(person1.transform.position, person2.transform.position - person1.transform.position, Color.green);
    }

    bool GetHitRayNone(Vector3 pos,Vector3 targetPos,GameObject obstacle)
    {
        // Rayを飛ばす：1から2に向けて
        Ray ray = new Ray(pos, targetPos - pos);
        RaycastHit hit;

        // Rayの可視化（デバッグ用）
        Debug.DrawRay(pos, targetPos - pos);

        // Rayが何かに衝突したか判定
        if (Physics.Raycast(ray, out hit))
        {
            // 衝突したオブジェクトが壁だった場合
            if (hit.collider.gameObject == obstacle)
            {
                Debug.Log("1から2は壁によって遮られています。");
                return false;
            }
            else
            {
                Debug.Log("1から2は見えます。");
                return true;
            }
        }
        else
        {
            return true;
        }
    }
}
