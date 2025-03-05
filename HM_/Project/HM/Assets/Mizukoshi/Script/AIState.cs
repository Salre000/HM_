using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
   // ó‘ÔŠÇ—
   public enum State
   {
        // ‘Ò‹@
        Idle,

        // ’Tõ
        Search,

        // ’ÇÕ
        Chase,

        // UŒ‚
        Attack,

        // ‰ñ”ğ‚·‚é
        Evade,
   }
    public State state;
}
