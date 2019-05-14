using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float moveSpeed = 8f;
    float rotationSpeed = 100f;

    Animator anim;
    Rigidbody rb;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        dir = new Vector3(h, 0f, v).normalized * moveSpeed;

        if (Input.GetMouseButton(1))
        {
            anim.SetFloat("DirV", v);
            anim.SetFloat("DirH", h);
            rb.MovePosition(transform.position + transform.TransformDirection(dir) * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("DirV", v);
            v *= moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + transform.forward * v);

            anim.SetFloat("DirH", h);
            h *= rotationSpeed * Time.deltaTime;
            rb.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * h));
        }

        if (h == 0 && v == 0)
        {
            anim.SetBool("Movement", false);
        }
        else
        {
            anim.SetBool("Movement", true);
        }
    }
}
