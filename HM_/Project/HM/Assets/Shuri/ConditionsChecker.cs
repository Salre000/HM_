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

    Coroutine finish;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        hunterManager = GetComponent<HunterManager>();
    }

    void Update()
    {
        if (finish != null) return;

        if (hunterManager.GetHunterDeathAmount() > 3)
        {
            finish = StartCoroutine(GoToResult(uiManager.GetLimitTime() - uiManager.remainingTime));
        }

        if(playerStatus.GetHP() <= 0 || uiManager.remainingTime <= 0)
        {
            finish = StartCoroutine(GoToResult(0));
        }
    }

    IEnumerator GoToResult(float time)
    {
        yield return new WaitForSeconds(3.0f);

        ResultRetention.SetClearTime(time);

        SceneManager.LoadScene("Result");
    }
}
