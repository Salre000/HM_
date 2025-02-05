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

    private void Start()
    {

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
        if (Physics.Raycast(startPos, transform.forward, out hit, watchDistance))
        {
            if (hit.transform.gameObject.name == "HandesIK")
            {

            }
                PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste != null) 
            {
                
                int ssz = 0;
            }
            //if (hit.transform.gameObject.tag=="Player")
            //{
            //    Debug.Log("”­Œ©");
            //}
            Debug.Log(hit.transform.gameObject);

        }
        else
        {
           
        }
        Debug.DrawLine(startPos, startPos + transform.forward*watchDistance, Color.red);
    }



}
