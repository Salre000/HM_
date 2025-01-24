using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public int menuIndex = 1;
    int menuNum = 4;

    public int sliderIndex;

    [SerializeField] TextAsset _option;

    [SerializeField] GameObject uiPanel;
    [SerializeField] GameObject beltText;
    [SerializeField] GameObject objective;

    [SerializeField] Slider[] slider;
    [SerializeField] Slider _sensibilityBar;
    [SerializeField] Slider _bgmBar;
    [SerializeField] Slider _seBar;
    [SerializeField] RectTransform cursor;

    float stopTime;

    InputManager _inputManager;

    UIManager _uiManager;

    void Start()
    {
        uiPanel.SetActive(false);

        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        string jsonText = _option.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        _sensibilityBar.value = float.Parse(json["sensibility"].Get<string>());
        _bgmBar.value = float.Parse(json["BGMvolume"].Get<string>());
        _seBar.value = float.Parse(json["SEvolume"].Get<string>());

        _uiManager.SetSliderValue(
            (int)_sensibilityBar.value,
            (int)_bgmBar.value,
            (int)_seBar.value);
    }

    void Update()
    {
        //stopTime -= Time.deltaTime;

        // オプション画面の開閉
        if (Input.GetKeyDown(_inputManager.config.start))
        {
            if (uiPanel.activeSelf)
            {
                _uiManager.SetSliderValue(
                    (int)_sensibilityBar.value,
                    (int)_bgmBar.value,
                    (int)_seBar.value);
            }
            uiPanel.SetActive(!uiPanel.activeSelf);
        }
        stopTime -= Time.deltaTime;
        Debug.Log(Time.deltaTime);
        // オプション画面が開いていたら
        if (uiPanel.activeSelf)
        {
            Time.timeScale = 0.0f;

            // RB
            if (Input.GetKeyDown(_inputManager.config.rb) && menuIndex < menuNum)
            {
                menuIndex++;
                UIMove(Vector3.left);
            }
            // LB
            if (Input.GetKeyDown(_inputManager.config.lb) && menuIndex > 1)
            {
                menuIndex--;
                UIMove(Vector3.right);
            }
        }
        else Time.timeScale = 1.0f;

        switch (menuIndex)
        {
            case 1: break;
            case 2:  break;
            case 3: Option(); break;
        }
    }

    private async UniTask UIMove(Vector3 dir)
    {
        for (int i = 0; i < 20; i++)
        {
            beltText.transform.position += dir * 250 / 20;
            objective.transform.position += dir * 1500 / 20;
            await UniTask.DelayFrame(1);
        }
    }

    private void FixedUpdate()
    {
        
        
    }

    void Option()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(slider[sliderIndex].gameObject);

        if (Input.GetAxis("D_Pad_V") > 0 && stopTime <= 0)
        {
            sliderIndex++;
            stopTime = 0.1f;
        }
        if (Input.GetAxis("D_Pad_V") < 0 && stopTime <= 0)
        {
            sliderIndex--;
            stopTime = 0.1f;
        }

        if (sliderIndex > slider.Length - 1) sliderIndex = 0;
        if (sliderIndex < 0) sliderIndex = slider.Length - 1;

        cursor.anchoredPosition = new Vector2(cursor.anchoredPosition3D.x, (1 - sliderIndex) * 100);
        
        slider[sliderIndex].value += Input.GetAxis("D_Pad_H");
    }
}
