using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTestHit : MonoBehaviour
{
    //�J�v�Z�������o���Q�_
    [SerializeField] GameObject _position1;
    [SerializeField] GameObject _position2;
    
    private CapsuleCollider _capsule;

    // Start is called before the first frame update
    void Start()
    {
        
        _capsule = this.gameObject.AddComponent<CapsuleCollider>();





    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
