using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnemySight2 : MonoBehaviour
{
    public float headTurnSpeed = 2f;  // Šç‚ðU‚é‘¬‚³
    private Vector3 targetRotation;

    void Start()
    {
        // ‰Šú‚Ìƒ^[ƒQƒbƒg‰ñ“]‚ðÝ’è
        SetRandomHeadRotation();
    }

    public void Update()
    {
        // Šç‚ðU‚éˆ—
        Vector3 direction = targetRotation - transform.eulerAngles;
        if (direction.magnitude > 0.1f)
        {
            // ‰ñ“]
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRotation, Time.deltaTime * headTurnSpeed);
        }
        else
        {
            // ƒ‰ƒ“ƒ_ƒ€‚È•ûŒü‚ÉŠç‚ðŒü‚¯‚é
            SetRandomHeadRotation();
        }
    }

    void SetRandomHeadRotation()
    {
        // ƒ‰ƒ“ƒ_ƒ€‚È•ûŒü‚ÉŠç‚ðŒü‚¯‚é
        float randomX = Random.Range(-30f, 30f);  
        float randomY = Random.Range(0f, 360f); 
        targetRotation = new Vector3(randomX, randomY, 0f);
    }
}
