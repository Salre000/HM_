using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItTest : MonoBehaviour
{
    private PlayerStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        _status = this.gameObject.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
