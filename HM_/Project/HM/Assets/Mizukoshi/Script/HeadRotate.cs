using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    private float _turnSpeed = 15.0f;  // Šç‚Ì‰ñ“]‘¬“x

    public float speedRate = 2.0f;

    public float maxY = 30.0f;
    public float minY = -30.0f;

    public float rotateAmount = 0;

    public float time = 0;

    private bool _fourCountChange=false;

    private bool _reverseRotate=false;

    void Start()
    {
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time>=2.0f/speedRate&&!_fourCountChange)
        {
            _fourCountChange = true;
            _reverseRotate = true;
            time = 0;
        }

        if (_fourCountChange)
        {
            if (time>=4.0f/speedRate)
            {

                if (_reverseRotate)
                {
                    _reverseRotate = false;
                }
                else
                {
                    _reverseRotate = true;
                }

                time = 0;
            }
        }

        if (_reverseRotate)
        {
            this.transform.Rotate(0f, _turnSpeed*speedRate*Time.deltaTime, 0f);
        }
        else
        {
            this.transform.Rotate(0f, -_turnSpeed*speedRate* Time.deltaTime, 0f);
        }
    }
}
