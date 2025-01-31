using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    [SerializeField] private GameObject m_gameObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        this.transform.position= m_gameObject.transform.position;
         this.transform.eulerAngles = m_gameObject.transform.eulerAngles;
    }
}
