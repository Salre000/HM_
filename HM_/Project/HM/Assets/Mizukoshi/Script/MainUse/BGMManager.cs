using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private SoundListManager soundListManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundListManager.instance.PlayBGM((int)BGM.MainGameBGM, (int)Main.BGM);
    }
}
