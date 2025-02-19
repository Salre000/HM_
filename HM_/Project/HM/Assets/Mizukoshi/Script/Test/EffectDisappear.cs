using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisappear : MonoBehaviour
{
    public GameObject effect;
    // Start is called before the first frame update
    public void ShowEffect()
    {
        effect.SetActive(true);
    }

    public void HideEffect()
    {
        effect.SetActive(false);
    }

    
}
