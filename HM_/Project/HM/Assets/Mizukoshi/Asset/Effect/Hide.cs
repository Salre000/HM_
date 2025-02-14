using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            this .gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            this .gameObject.SetActive(false);
        }
    }

    public void Active()
    {
        this .gameObject.SetActive(true);
    }
}
