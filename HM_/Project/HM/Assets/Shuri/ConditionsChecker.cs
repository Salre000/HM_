using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionsChecker : MonoBehaviour
{
    HunterManager hunterManager;
    PlayerStatus playerStatus;
    [SerializeField] UIManager uiManager;

    // 終了フラグ
    private bool _finishFlag;

    private void Start()
    {
        _finishFlag = false;

        // オブジェクトを探してクラスを取得
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        hunterManager = GameObject.Find("GameManager").GetComponent<HunterManager>();
    }

    void Update()
    {
        if (_finishFlag) return;

        if (hunterManager.GetHunterDeathAmount() > 3)
        {
            GoToResult(uiManager.GetLimitTime() - uiManager.remainingTime).Forget();
        }

        if(playerStatus.GetHP() <= 0 || uiManager.remainingTime <= 0)
        {
            GoToResult(0).Forget();
        }
    }

    private async UniTask GoToResult(float time)
    {
        _finishFlag = true;

        await UniTask.DelayFrame(Application.targetFrameRate * 3);

        ResultRetention.SetClearTime(time);

        SceneManager.LoadScene("Result");
    }
}
