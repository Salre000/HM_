using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonItem : MonoBehaviour
{
    private void Start()
    {


    }

    [SerializeField] private GameObject dragItemRock;
    public GameObject GetObjectRock()
    {
        return dragItemRock; 
    }

}
