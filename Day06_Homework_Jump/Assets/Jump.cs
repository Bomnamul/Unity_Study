using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    bool isJumping = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jumping());
        }
    }

    IEnumerator Jumping()
    {
        isJumping = true;
        print("Jump1");
        rb.AddForce(Vector3.up * 200f);
        yield return new WaitForSeconds(1f);
        print("Jump2");
        rb.AddForce(Vector3.up * 200f);
        yield return new WaitForSeconds(1f);
        print("Jump3");
        rb.AddForce(Vector3.up * 500f);
        rb.angularVelocity = Vector3.up * 30;
        rb.angularDrag = 2;
        yield return new WaitForSeconds(2f);
        rb.angularDrag = 0;
        isJumping = false;
    }
}
