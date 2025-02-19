using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject _hpmanager;

    public int defeatCountTrigger = 4;

    // Start is called before the first frame update
    void Start()
    {
        _hpmanager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        // ÉvÉåÉCÉÑÅ[ÇÃHPÇ™0Ç»ÇÁÇŒ
        if (_hpmanager.GetComponent<HPManager>().GetMonsterHp()<=0)
        {
            Debug.Log("GameOver");
        }
        if(this.gameObject.GetComponent<HunterManager>().GetHunterDeathAmount() >= defeatCountTrigger)
        {
            Debug.Log("GameClear");
        }
    }
}
