using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject player;

    public int defeatCountTrigger = 4;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // ÉvÉåÉCÉÑÅ[ÇÃHPÇ™0Ç»ÇÁÇŒ
        if (player.GetComponent<PlayerStatus>().GetHP() <= 0)
        {
            Debug.Log("GameOver");
        }
        if(this.gameObject.GetComponent<HunterManager>().GetHunterDeathAmount() >= defeatCountTrigger)
        {
            Debug.Log("GameClear");
        }
    }
}
