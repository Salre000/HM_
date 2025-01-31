using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// s“®˜_—
/// 
/// ‡@“G‚ğŒ©‚Â‚¯‚é‚Ü‚Å‚Íœpœj
/// 
/// ‡A“G‚ğŒ©‚Â‚¯‚½‚ç2”Ô–Ú‚É‹ß‚¢‚Æ‚±‚ë‚ÉˆÚ“®
/// 
/// ‡B‘_Œ‚(cool time‚Í5•b)
/// 
/// ‡C“G‚ª‹ß‚Ã‚¢‚Ä‚«‚½‚ç“¦‚°‚é ‚à‚µ‚­‚Í‹ß‹——£UŒ‚
/// 
/// ‡DˆÈ~ŒJ‚è•Ô‚µ
/// 
/// 
/// 
/// </summary>

// ‹|‚ÌAI‚Ìs“®˜_—
public class AI2Logic :Hunter_AI
{
    [SerializeField]
    // UŒ‚‹——£
    private float attackDistance = 20.0f;

    [SerializeField]
    private float attackCoolTime = 5.0f;

    private void Start()
    {
        SetAttackDistance(attackDistance);
        SetAttackCoolTime(attackCoolTime);
    }

    private void Update()
    {
        // S‘©ó‘Ô‚È‚ç‚Î“®‚©‚È‚¢
        if (CheckRest())return;

        if (!monsterDisplay)
        {
            Search();
        }
        else
        {

        }
    }
}
