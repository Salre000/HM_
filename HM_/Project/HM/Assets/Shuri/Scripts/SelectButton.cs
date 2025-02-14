using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] Button button;
    
    [SerializeField]Camera _demoCamDragon;
    [SerializeField]Camera _demoCamSpider;

    [SerializeField] RenderTexture _renderTexture;

    [SerializeField] DemoPlayer _dragon;
    [SerializeField] DemoPlayer _spider;
    void Update()
    {
        if (Input.GetAxis("D_Pad_V") > 0)
        {
            _dragon.ResetAnime();
            _demoCamSpider.targetTexture = null;
            _demoCamDragon.targetTexture = _renderTexture;
            //button.navigation.selectOnUp.Select();
            //button = button.navigation.selectOnUp.GetComponent<Button>();
        }
        if (Input.GetAxis("D_Pad_V") < 0)
        {
            _spider.ResetAnime();
            _demoCamDragon.targetTexture = null;
            _demoCamSpider.targetTexture = _renderTexture;
            //button.navigation.selectOnDown.Select();
            //button = button.navigation.selectOnDown.GetComponent<Button>();
        }
    }
}
