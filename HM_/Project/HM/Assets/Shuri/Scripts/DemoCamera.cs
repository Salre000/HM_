using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCamera : MonoBehaviour
{
    [SerializeField] GameObject target;

    float _angle = Mathf.PI;

    const float Distance = 35.0f;

    void Update()
    {
        transform.LookAt(target.transform);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Sin(_angle) * Distance, 25, Mathf.Cos(_angle) * Distance);

        _angle -= 0.3f * Mathf.PI / 180.0f;
    }
}
