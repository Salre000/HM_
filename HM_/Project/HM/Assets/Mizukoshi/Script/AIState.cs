using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
   // ó‘ÔŠÇ—
   public enum State
   {
        Idle,
        Search,
        Chase,
        Attack,
        Evade,
   }
    public State state;
}
