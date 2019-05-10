using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool onGround;
    float jumpPressure;
    float minJump;
    float maxJumpPressure;

    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        jumpPressure = 0f;
        minJump = 2f;
        maxJumpPressure = 10f;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            if (Input.GetButton("Jump"))
            {
                if (jumpPressure < maxJumpPressure)
                {
                    jumpPressure += 10f * Time.deltaTime;
                }
                else
                {
                    jumpPressure = maxJumpPressure;
                }
                anim.SetFloat("JumpPressure", jumpPressure + minJump);
                anim.speed = 1f + (jumpPressure * 0.15f); // 비례를 확인 (극단 값을 주면 이해하기 쉬움)
            }
            else
            {
                if (jumpPressure > 0f)
                {
                    jumpPressure += minJump;
                    rb.velocity = new Vector3(0f, jumpPressure, 0f);
                    jumpPressure = 0f;
                    onGround = false;
                    anim.SetFloat("JumpPressure", 0f);
                    anim.SetBool("OnGround", onGround);
                    anim.speed = 1f;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            anim.SetBool("OnGround", true);
        }
    }
}
