using Cysharp.Threading.Tasks;
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

    [SerializeField] Image _timer;
    [SerializeField] Slider _hpBar;
    [SerializeField] Image conditionImage;

    [SerializeField] TextMeshProUGUI _textMeshProUGUI;

    [SerializeField] HPManager _hpManager;
    [SerializeField] HunterManager _hunterManager;

    [SerializeField] Sprite[] _conditionSprites;

    void Start()
    {
        remainingTime = LimitTime;

        _hpBar.value = _hpBar.maxValue = _hpManager.GetMaxMonsterHp();

        GameObject.FindWithTag("Player").GetComponent<PlayerStatus>().SetCallBackCondition(ChangeConditionSprite);
    }

    ~UIManager()
    {

    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        remainingTime -= Time.deltaTime;

        _timer.transform.eulerAngles = new Vector3(0, 0, remainingTime / LimitTime * 360.0f);

        if (remainingTime <= 0) Debug.Log("終了");
    }

    public async UniTask HPSliderUpdate()
    {
        float currentHp = _hpBar.value;
        float elapsedTime = 0;
        float targetHp = _hpManager.GetMonsterHp();

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;

            _hpBar.value = Mathf.Lerp(currentHp, targetHp, elapsedTime / 0.5f);

            await UniTask.DelayFrame(1);
        }
    }

    public void ObjectiveText()
    {
        _textMeshProUGUI.text = string.Format("▼ハンターを4体倒す {0}/4", _hunterManager.GetHunterDeathAmount());
    }

    private void ChangeConditionSprite(PlayerStatus.Condition condition)
    {
        switch (condition)
        {
            case PlayerStatus.Condition.Normal:
                conditionImage.sprite = _conditionSprites[(int)PlayerStatus.Condition.Normal]; break;
            case PlayerStatus.Condition.Stun:
                conditionImage.sprite = _conditionSprites[(int)PlayerStatus.Condition.Stun]; break;
            case PlayerStatus.Condition.Anger:
                conditionImage.sprite = _conditionSprites[(int)PlayerStatus.Condition.Anger]; break;
            case PlayerStatus.Condition.Fatigue:
                conditionImage.sprite = _conditionSprites[(int)PlayerStatus.Condition.Fatigue]; break;
        }
    }

    public float GetLimitTime()
    {
        return LimitTime;
    }
}
