using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionsChecker : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;
    HunterManager _hunterManager;
    HPManager _hpManager;

    // 終了フラグ
    private bool _finishFlag;

    private void Start()
    {
        _finishFlag = false;

        _hunterManager = GetComponent<HunterManager>();
        _hpManager = GetComponent<HPManager>();
    }

    void Update()
    {
        if (_finishFlag) return;

        // ハンターのやられた回数が4回以上だったら
        if (_hunterManager.GetHunterDeathAmount() >= 4)
        {
            GoToResult(_uiManager.GetLimitTime() - _uiManager.remainingTime).Forget();
        }

        // プレイヤーのHPが0以下になったら
        if (_hpManager.GetMonsterHp() <= 0 || _uiManager.remainingTime <= 0)
        {
            GoToResult(0).Forget();
        }
    }

    private async UniTask GoToResult(float time)
    {
        _finishFlag = true;

        // クリアタイムの記録
        ResultRetention.SetClearTime(time);

        // 3秒待機
        await UniTask.DelayFrame(180);

        // シーンの移動
        SceneManager.LoadScene("Result");
    }
}
