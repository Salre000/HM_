using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnddestory : MonoBehaviour
{
    private Vector3 _dir;
    public GameObject _object;
    public GameObject _object2;

    public GameObject _turn;

    // Start is called before the first frame update
    void Start()
    {
        Turn();
        SetDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(_dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    
    public void SetDirection()
    {
        //_dir=(-_object.transform.localPosition+_object2.transform.localPosition).normalized;
        _dir=(_turn.transform.position-this.transform.position).normalized;
    }

    public void Turn()
    {
        if(_turn==null) return;
        this.transform.LookAt(_turn.transform);
    }
}
