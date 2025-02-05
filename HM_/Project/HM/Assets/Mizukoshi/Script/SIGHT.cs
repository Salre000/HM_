using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIGHT : MonoBehaviour
{
    [SerializeField]
    private float watchDistance = 20;
    [SerializeField]
    private float verticalAngle = 80;
    [SerializeField]
    private float horizonAngle = 120;

    private Vector3 startPos;
    private Vector3 endPos;
    private RaycastHit hit;

    Vector3 direction;

    private GameObject _monster;

    private void Start()
    {
        _monster = GameObject.FindGameObjectWithTag("Player");
        startPos.y += 0.75f;
       
    }

    private void Update()
    {
        startPos = transform.position;
        startPos.y += 0.75f;
        direction = watchDistance * transform.forward;
        Check();
    }

    void Check()
    {
        Vector3 dir=(_monster.transform.position - startPos).normalized;
        //Vector3 LookDir=transform.TransformDirection(Vector3.forward);
        float y=this.transform.eulerAngles.y;
        //Vector3 LookDir = new Vector3(Mathf.Sin(y*Mathf.Deg2Rad)*30,0,Mathf.Cos(y*Mathf.Deg2Rad)*30);
        Vector3 LookDir= transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(startPos,dir, out hit, watchDistance))
        {
            if (hit.transform.gameObject.name == "HandesIK")
            {

            }
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste != null) 
            {
                
                int ssz = 0;
                Debug.Log(hit.transform.gameObject);
            }
            
            

            //if (hit.transform.gameObject.tag=="Player")
            //{
            //    Debug.Log("”­Œ©");
            //}
           

        }
        else
        {
           
        }
        Debug.DrawLine(startPos, dir * watchDistance, Color.red);
        Debug.DrawLine(startPos, startPos+LookDir*watchDistance , Color.blue);
        Debug.Log("Šp“x:"+Vector3.Angle(dir,LookDir));
    }



}
