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

    UniTask _panelMoveTask = UniTask.CompletedTask;
    UniTask _cursorMoveTask = UniTask.CompletedTask;

    void Start()
    {
        uiPanel.SetActive(false);

        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        string jsonText = _option.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        cursor.anchoredPosition = new(cursor.anchoredPosition3D.x, (1 - sliderIndex) * 100);

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
            if (Input.GetKeyDown(_inputManager.config.rb) && menuIndex < menuNum && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex++;
                _panelMoveTask = UIMove(Vector3.left);
            }
            // LB
            if (Input.GetKeyDown(_inputManager.config.lb) && menuIndex > 1 && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex--;
                _panelMoveTask = UIMove(Vector3.right);
            }
        }
        else Time.timeScale = 1.0f;

        switch (menuIndex)
        {
            case 1: break;
            case 2: break;
            case 3: Option(); break;
        }
    }

    private async UniTask UIMove(Vector3 dir)
    {
        Vector3 beltPos = beltText.transform.position;
        Vector3 objectivePos = objective.transform.position;

        for (int i = 0; i < 10; i++)
        {
            beltText.transform.position += dir * 250 / 10;
            objective.transform.position += dir * 1500 / 10;
            await UniTask.DelayFrame(1);
        }

        beltText.transform.position = beltPos + dir * 250;
        objective.transform.position = objectivePos + dir * 1500;
    }


    void Option()
    {
        if (!_cursorMoveTask.Status.IsCompleted()) return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(slider[sliderIndex].gameObject);

        slider[sliderIndex].value += Input.GetAxis("D_Pad_H");

        if (Input.GetAxis("D_Pad_V") > 0 || Input.GetAxis("Vertical") > 0)
        {
            sliderIndex--;
        }
        if (Input.GetAxis("D_Pad_V") < 0 || Input.GetAxis("Vertical") < 0)
        {
            sliderIndex++;
        }
        if (Input.GetAxis("D_Pad_V") == 0 && Input.GetAxis("Vertical") == 0) return;

        if (sliderIndex > slider.Length - 1) sliderIndex = 0;
        if (sliderIndex < 0) sliderIndex = slider.Length - 1;

        _cursorMoveTask = ChangeSelectSlider();
    }

    private async UniTask ChangeSelectSlider()
    {
        Vector2 startPos = cursor.anchoredPosition;
        Vector2 goalPos = new(cursor.anchoredPosition3D.x, (1 - sliderIndex) * 100);

        Debug.Log("WWW Start" + startPos);
        Debug.Log("WWW Goal" + goalPos);
        Debug.Log("WWW index" + sliderIndex);

        for (float i = 0; i < 10; i++)
        {
            cursor.anchoredPosition = Vector2.Lerp(startPos, goalPos, (i + 1 / 10.0f));
            Debug.Log("WWW t = " + i / 10);
            await UniTask.DelayFrame(1);
        }
        cursor.anchoredPosition = goalPos;
    }
}
