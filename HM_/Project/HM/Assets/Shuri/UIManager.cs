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

    [SerializeField] Image timer;
    [SerializeField] Slider hpBar;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] HunterManager hunterManager;

    void Start()
    {
        remainingTime = LimitTime;

        hpBar.value = hpBar.maxValue = playerStatus.GetMaxHP();

        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
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

        timer.fillAmount = remainingTime / LimitTime;

        if (remainingTime <= 0) Debug.Log("�I��");
    }

    void HPBar()
    {
        hpBar.value = playerStatus.GetHP();
    }

    void ObjectiveText()
    {
        textMeshProUGUI.text = string.Format("��Defeat the Hunter {0}/4", hunterManager.GetHunterDeathAmount());
    }

    public float GetLimitTime()
    {
        return LimitTime;
    }
}
