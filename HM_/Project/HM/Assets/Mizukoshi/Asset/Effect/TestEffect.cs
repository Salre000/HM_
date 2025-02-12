using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : MonoBehaviour
{
    public GameObject effect;
    private GameObject setPos;
    // Start is called before the first frame update
    void Start()
    {
        setPos = GameObject.FindGameObjectWithTag("EnemyAttack");
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetEffect();
        }
    }

    // Update is called once per frame
    public void SetEffect()
    {
        GameObject clone= Instantiate(effect,setPos.transform.position,setPos.transform.rotation);
        clone.transform.SetParent(setPos.transform);
    }
}
