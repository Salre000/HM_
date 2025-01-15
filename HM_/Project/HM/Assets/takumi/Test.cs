using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] HItTest []hItTest=new HItTest[30];
    [SerializeField]GameObject []GameObject=new GameObject[30];
    // Start is called before the first frame update
    void Start()
    {
        hItTest = FindObjectsOfType<HItTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
