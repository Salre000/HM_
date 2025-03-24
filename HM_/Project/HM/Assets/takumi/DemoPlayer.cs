using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ResetAnime()
    {

        animator.SetTrigger("6");

        time = 0;
    }

    public float MaxTime=10;

    public float  time = 0;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > MaxTime) 
        {
            time = 0;

            int rand=Random.Range(1, 6);


            switch (rand) 
            {
                 
                case 1:
                    animator.SetBool("1", true); ResetFlag().Forget();
                    break;
                case 2:
                    animator.SetBool("2", true); ResetFlag().Forget();
                    break;
                case 3:
                    animator.SetBool("3", true); ResetFlag().Forget();
                    break;
                case 4:
                    animator.SetBool("4", true); ResetFlag().Forget();
                    break;
                case 5:
                    animator.SetBool("5", true); ResetFlag().Forget();
                    break;


            }

        }

    }

    private async UniTask ResetFlag() 
    {
        await UniTask.DelayFrame(1);
        animator.SetBool("1", false);
        animator.SetBool("2", false);
        animator.SetBool("3", false);
        animator.SetBool("4", false);
        animator.SetBool("5", false);




    }
}
