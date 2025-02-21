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

    // �I���t���O
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

        // �n���^�[�̂��ꂽ�񐔂�4��ȏゾ������
        if (_hunterManager.GetHunterDeathAmount() >= 4)
        {
            GoToResult(_uiManager.GetLimitTime() - _uiManager.remainingTime).Forget();
        }

        // �v���C���[��HP��0�ȉ��ɂȂ�����
        if (_hpManager.GetMonsterHp() <= 0 || _uiManager.remainingTime <= 0)
        {
            GoToResult(0).Forget();
        }
    }

    private async UniTask GoToResult(float time)
    {
        _finishFlag = true;

        // �N���A�^�C���̋L�^
        ResultRetention.SetClearTime(time);

        // 3�b�ҋ@
        await UniTask.DelayFrame(180);

        // �V�[���̈ړ�
        SceneManager.LoadScene("Result");
    }
}
