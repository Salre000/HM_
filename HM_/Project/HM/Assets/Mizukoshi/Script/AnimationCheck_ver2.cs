using Den.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck_ver2 : MonoBehaviour
{
    GameObject Object;

    private void Start()
    {
        Object = this.gameObject;
        Object.GetComponent<HunterAnimation>().Idle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Flater");
            Object.GetComponent<HunterAnimation>().Falter();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Attack");
            Object.GetComponent<HunterAnimation>().Attack();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Death");
            Object.GetComponent<HunterAnimation>().Death();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Rolling");
            Object.GetComponent<HunterAnimation>().Walk();
        }
    }
}
