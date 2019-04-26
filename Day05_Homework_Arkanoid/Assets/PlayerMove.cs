using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 10f;

    Rigidbody rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        h *= speed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position - transform.up * h);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.DetachChildren();
        }
    }
}
