using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TripleJumpAttack()); // func호출 x, delegate callback
        }
    }

    IEnumerator TripleJumpAttack()
    {
        Jump(2f);
        yield return new WaitForSeconds(1.8f);
        Jump(2f);
        yield return new WaitForSeconds(1.8f);
        yield return WheelWindJump(5f);
    }

    IEnumerator WheelWindJump(float height) // 왜 바닥에 닿자마자 멈추는지 알아보자
    {
        Jump(height);
        rb.maxAngularVelocity = 100;
        rb.angularVelocity = Vector3.up * 30f;
        yield return new WaitForSeconds(3f);
        rb.angularDrag = 2f;
        yield return new WaitForSeconds(2f);
        rb.angularVelocity = Vector3.zero;
        rb.angularDrag = 0.05f;
    }

    private void Jump(float height)
    {
        Vector3 v = rb.velocity;
        v.y = Mathf.Sqrt(-2.0f * Physics.gravity.y * height); // 정확히 height 높이까지 점프하는 velocity 값 (외워둡시다)
        //rb.velocity = v;
        rb.AddForce(v, ForceMode.VelocityChange); // line:42와 같은 방법
    }
}
