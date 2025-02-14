using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST : MonoBehaviour
{
    public SoundListManager soundListManager;

    

    private void Start()
    {
        soundListManager.PlaySound((int)main.hunter, (int)HunterSE.WalkSE);
    }
}
