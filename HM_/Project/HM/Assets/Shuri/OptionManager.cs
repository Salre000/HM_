using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public int menuIndex = 1;
    int menuNum = 4;

    public int sliderIndex;

    [SerializeField] GameObject uiPanel;
    [SerializeField] GameObject beltText;
    [SerializeField] GameObject objective;

    [SerializeField] Slider[] slider;
    [SerializeField] RectTransform cursor;

    InputManager _inputManager;

    void Start()
    {
        uiPanel.SetActive(false);

        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {
        // オプション画面の開閉
        if (Input.GetKeyDown(_inputManager.config.start))
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
        }

        // オプション画面が開いていたら
        if(uiPanel.activeSelf)
        {
            Time.timeScale = 0.0f;

            // RB
            if (Input.GetKeyDown(_inputManager.config.rb) && menuIndex < menuNum) 
            {
                menuIndex++;
                StartCoroutine(UIMove(Vector3.left));
            }
            // LB
            if (Input.GetKeyDown(_inputManager.config.lb) && menuIndex > 1) 
            {
                menuIndex--;
                StartCoroutine(UIMove(Vector3.right));
            }
        }
        else Time.timeScale = 1.0f;

        switch (menuIndex)
        {
            case 1:break;
            case 2:Option(); break;
            case 3:break;
        }
    }

    IEnumerator UIMove(Vector3 dir)
    {
        for (int i = 0; i < 20; i++) 
        {
            beltText.transform.position += dir * 250 / 20;
            objective.transform.position += dir * 1500 / 20;
            yield return new WaitForEndOfFrame();
        }
    }

    void Option()
    {
        if (Input.GetAxis("D_Pad_V") > 0) sliderIndex++;
        if (Input.GetAxis("D_Pad_V") < 0) sliderIndex--;

        if (sliderIndex > slider.Length - 1) sliderIndex = 0;
        if (sliderIndex < 0) sliderIndex = slider.Length - 1;

        cursor.anchoredPosition = new Vector2(cursor.anchoredPosition3D.x, (1 - sliderIndex) * 100);

        slider[sliderIndex].value += Input.GetAxis("D_Pad_H");
    }
}
