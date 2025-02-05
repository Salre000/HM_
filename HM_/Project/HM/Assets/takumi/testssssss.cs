using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class testssssss : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Material material;
    public Gradient aaaa;
    // Start is called before the first frame update
    void Start()
    {
        material.color = Color.white;

        lineRenderer = this.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.startWidth =lineRenderer.endWidth= 0.1f;
        lineRenderer.colorGradient = aaaa;
        lineRenderer.material = material;

        lineRenderer.SetPosition(0, this.transform.position);



    }

    // Update is called once per frame
    void Update()
    {

        float range = Vector3.Distance(this.transform.position,lineRenderer.GetPosition(lineRenderer.positionCount-1));

        if (range <= 0.02) return;     
        

        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, this.transform.position);


    }
}
