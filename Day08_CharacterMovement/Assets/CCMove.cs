﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpHeight = 2f;
    public LayerMask groundMask;
    public Transform groundChecker;

    CharacterController cc;
    Vector3 velocity;
    [SerializeField]
    bool isGrounded = true;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, 0.5f, groundMask, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        move = move.normalized;
        cc.Move(move * moveSpeed * Time.deltaTime); // 한 프레임에 이동해야 할 Vector
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // 물리 시뮬레이션 구현
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
        velocity.y += Physics.gravity.y * Time.deltaTime; // 중력 구현
        cc.Move(velocity * Time.deltaTime); // Move 하나로 합칠 수 있다
    }

    // 주어진 힘을 가지고 구현
    // Time.deltaTime을 두 번 곱하는 이유: 

    // p(i + 1) = p(i) + v(i + 1) * dt
    // v(i + 1) = v(i) + a(i + 1) * dt : 속도의 정의
    // a(i + 1) = F(i + 1) / m         : 가속도의 정의 : F = m * a   =>   F = m * g(중력)

    // p = position, v = velocity(시간에 대한 거리의 변화율), a = accelation(가속도), dt = deltaTime

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(groundChecker.position, 0.5f);
    }

    // rigidbody가 없어 충돌확인을 처리 해줘야 함
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var h = hit.gameObject.GetComponent<HealingPlatform>();
        if (h != null)
        {
            GetComponent<HealOverTime>().Heal();
        }
    }
}
