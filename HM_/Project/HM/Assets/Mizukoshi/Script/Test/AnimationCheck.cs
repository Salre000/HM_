using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck : MonoBehaviour
{
    private Animator animator;
    private CharacterController cCon;
    private Vector3 velocity;
    [SerializeField]
    private float walkSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        cCon = GetComponent<CharacterController>();
        velocity = Vector3.zero;
    }

    void Update()
    {

        //　地面に接地してる時は初期化
        if (cCon.isGrounded)
        {
            velocity = Vector3.zero;

            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //　方向キーが多少押されている
            if (input.magnitude > 0f
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
            )
            {
                animator.SetFloat("Speed", input.magnitude);

                transform.LookAt(transform.position + input);

                velocity += input.normalized * walkSpeed;
                //　キーの押しが小さすぎる場合は移動しない
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }

            if (Input.GetButtonDown("Fire1")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
                && !animator.IsInTransition(0)
            )
            {
                animator.SetTrigger("Attack2");
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        cCon.Move(velocity * Time.deltaTime);
    }
}
