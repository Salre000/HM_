using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{
    Vector3 MoveVec;

    float StartUpVec = 0.03f;
    float DVec = -0.001f;
    public void SetMoveVec(Vector3 Vec) { MoveVec = Vec; }


    [SerializeField] private Tag TagBox;

    float TimeCount = 0;
    // Start is called before the first frame update

    RockPool RockPool;

    private void Start()
    {

        RockPool=GameObject.FindGameObjectWithTag("ItemBox").GetComponent<RockPool>();

    }

    private void FixedUpdate()
    {

        TimeCount += Time.deltaTime;
        this.transform.position+=MoveVec/10;

        this.transform.position += new Vector3(0,StartUpVec,0);

        StartUpVec += DVec;

        if (TimeCount > 3) 
        {
            TimeCount = 0;
            RockPool.AddActiveCount();
            StartUpVec = 0.05f;
            
            this.gameObject.SetActive(false);


        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == TagBox.GetEnemyTag())
        {
            RockPool.AddActiveCount();
            this.gameObject.SetActive(false);

        }

    }

}
