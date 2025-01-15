using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI3Animation : MonoBehaviour
{
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void ResetAnimation()
    {

    }
}
