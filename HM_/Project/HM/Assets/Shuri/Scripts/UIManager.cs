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

    [SerializeField] HPManager _hpManager;

    [SerializeField] Sprite[] _conditionSprites;

    async void Start()
    {
        Time.timeScale = 0.0f;

        remainingTime = LimitTime;

        _hpBar.value = _hpBar.maxValue = _hpManager.GetMaxMonsterHp();

        GameObject.FindWithTag("Player").GetComponent<PlayerStatus>().SetCallBackCondition(ChangeConditionSprite);

        await UniTask.WaitUntil(() => CameraManager.setupFlag);

        Time.timeScale = 1.0f;
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        remainingTime -= Time.deltaTime;

        _timer.transform.eulerAngles = new Vector3(0, 0, remainingTime / LimitTime * 360.0f);

        if (remainingTime <= 0) Debug.Log("I—¹");
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
