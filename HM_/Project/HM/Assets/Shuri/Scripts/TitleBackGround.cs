using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBackGround : MonoBehaviour
{
    [SerializeField] Sprite[] backGroundSprites;

    void Start()
    {
        GetComponent<Image>().sprite = backGroundSprites[Random.Range(0, backGroundSprites.Length - 1)];
    }
}
