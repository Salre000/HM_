using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class UIManager : MonoBehaviour
{
    public const float LimitTime = 600;

    public float remainingTime;

    int _sensibility;
    int _bgmVolume;
    int _seVolume;

    [SerializeField] Image _timer;
    [SerializeField] Slider _hpBar;
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;

    [SerializeField] HPManager _hpManager;
    [SerializeField] HunterManager _hunterManager;

    void Start()
    {
        remainingTime = LimitTime;

        _hpBar.value = _hpBar.maxValue = _hpManager.GetMaxMonsterHp();
    }

    void Update()
    {
        Timer();
        HPBar();
        ObjectiveText();
    }

    void Timer()
    {
        remainingTime -= Time.deltaTime;

        _timer.fillAmount = remainingTime / LimitTime;

        if (remainingTime <= 0) Debug.Log("I—¹");
    }

    void HPBar()
    {
        _hpBar.value = _hpManager.GetMonsterHp();
    }

    void ObjectiveText()
    {
        _textMeshProUGUI.text = string.Format("¥Defeat the Hunter {0}/4", _hunterManager.GetHunterDeathAmount());
    }

    public float GetLimitTime()
    {
        return LimitTime;
    }

    public int GetSensibility()
    {
        return _sensibility;
    }
}
