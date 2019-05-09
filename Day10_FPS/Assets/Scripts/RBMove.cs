using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpHeight = 2f;
    public float slopeLimit = 45;
    public LayerMask groundMask;
    public Transform groundChecker;

    Rigidbody rb;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    bool isGrounded = false;
    [SerializeField]
    bool onSlidingSlope = false;
    bool isJumping = false;
    RaycastHit hit;

    Vector3 hitNormal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 생각보다 느린 함수라 Start()에 해주는게 좋다
    }

    // Update is called once per frame
    void Update()
    {
        // 1: Ray를 쏠 때 바닥에 틈이 있을경우 문제가 발생

        //var ray = new Ray(transform.position, -transform.up);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 1f + 0.1f))
        //{
        //    isGrounded = true;
        //}
        //else
        //{
        //    isGrounded = false;
        //}
        //Debug.DrawRay(ray.origin, ray.direction, Color.magenta, 0.2f);

        // 2: Checker를 만들어 Sphere가 닿는지를 판단해 isGrounded 결정, Check 시리즈가 여러 개 있다
        //isGrounded = Physics.CheckSphere(groundChecker.position, 0.5f, groundMask, QueryTriggerInteraction.Ignore);

        // 3: Ray가 생성될 때 이미 겹쳐있는 경우는 hit판정 안됨 (안쪽에는 면이 없기 때문에 ray를 쏘아도 hit x)
        isGrounded = Physics.SphereCast(groundChecker.position, 0.5f, -transform.up, out hit, 0.2f, groundMask, QueryTriggerInteraction.Ignore);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartCoroutine(Jumping());
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(h, 0f, v).normalized;
        moveDirection *= moveSpeed;
        transform.LookAt(transform.position + moveDirection);

        onSlidingSlope = Vector3.Angle(Vector3.up, hit.normal) > slopeLimit;
        print(Vector3.Angle(Vector3.up, hit.normal));

        if (!onSlidingSlope && isGrounded && !isJumping)
        {
            rb.drag = 100;
        }
        else
        {
            rb.drag = 0;
        }
    }

    IEnumerator Jumping()
    {
        isJumping = true;
        rb.drag = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }

    private void FixedUpdate()
    {
        Vector3 move = moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(groundChecker.position, 0.5f);
    }
}
