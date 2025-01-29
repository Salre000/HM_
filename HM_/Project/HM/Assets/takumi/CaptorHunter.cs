using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptorHunter : MonoBehaviour
{

    System.Action<GameObject> SetObject;

    public void SetGameObject(System.Action<GameObject> SetObject) { this.SetObject = SetObject; }

    bool activeFlag = false;    

    public void SetActiveFlag(bool Flag) {  activeFlag = Flag; }

    private void Start()
    {
        this.gameObject.SetActive(activeFlag);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!activeFlag) return;

        GameObject Parent=other.gameObject;

        Debug.Log(Parent.tag);

        if (Parent.tag != "Hunter") return;

        SetObject(other.gameObject);

        activeFlag = false;

       



    }

}
