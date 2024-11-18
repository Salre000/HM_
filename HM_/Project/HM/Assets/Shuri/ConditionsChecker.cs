using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionsChecker : MonoBehaviour
{
    [SerializeField] HunterManager hunterManager;
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] UIManager uiManager;
    [SerializeField] ResultRetention resultRetention;

    void Update()
    {
        if (hunterManager.GetHunterDeathAmount() > 3)
        {
            StartCoroutine(GoToResult(true, uiManager.GetLimitTime() - uiManager.remainingTime));
        }

        if(playerStatus.GetHP() <= 0 || uiManager.remainingTime <= 0)
        {
            StartCoroutine(GoToResult(false, 0));
        }
    }

    IEnumerator GoToResult(bool clearFlag,float time)
    {
        yield return new WaitForSeconds(3.0f);

        resultRetention.SetResultData(clearFlag, time);

        SceneManager.LoadScene("Result");
    }
}
