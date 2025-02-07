using Cysharp.Threading.Tasks;
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

    private void FixedUpdate()
    {

        this.transform.position+=MoveVec/100;

        this.transform.position += new Vector3(0,StartUpVec/10,0);

        StartUpVec += DVec;

        
    }

    public async UniTask ActiveChenge() 
    {
        await UniTask.WaitForSeconds(5);

            StartUpVec = 0.05f;
            
            this.gameObject.SetActive(false);





    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == TagBox.GetEnemyTag())
        {
            this.gameObject.SetActive(false);

        }

    }

}
