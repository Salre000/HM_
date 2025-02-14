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
        animator.SetFloat("Rand", -1);

        if (time > MaxTime) 
        {
            time = 0;

            int rand=Random.Range(1, 6);


            switch (rand) 
            {
                 
                case 1:
                    animator.SetBool("1", true);
                    break;
                case 2:
                    animator.SetBool("2", true);
                    break;
                case 3:
                    animator.SetBool("3", true);
                    break;
                case 4:
                    animator.SetBool("4", true);
                    break;
                case 5:
                    animator.SetBool("5", true);
                    break;


            }

        }
        else if(time>1)
        {
            animator.SetBool("1", false);
            animator.SetBool("2", false);
            animator.SetBool("3", false);
            animator.SetBool("4", false);
            animator.SetBool("5", false);

        }


    }
}
