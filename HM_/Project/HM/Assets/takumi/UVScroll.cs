using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{
    [SerializeField] Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 offset = Vector2.zero;

    private void FixedUpdate()
    { 
        material.SetTextureOffset("_MainTex", offset);
        offset.x += 0.01f;
        offset.y -= 0.01f;
    }
}
