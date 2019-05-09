using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int moveSpeed;

    Animator anim;
    Rigidbody rb;
    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            moveSpeed = 10;
            anim.SetInteger("Speed", moveSpeed);
        }
        else
        {
            moveSpeed = 0;
            anim.SetInteger("Speed", moveSpeed);
        }

        moveDir = new Vector3(h, 0f, v).normalized;
        moveDir *= moveSpeed;

        rb.MovePosition(rb.position + moveDir * Time.deltaTime);
        transform.LookAt(transform.position + moveDir);
    }
}
