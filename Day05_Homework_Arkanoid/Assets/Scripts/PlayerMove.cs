using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 20f;

    Rigidbody rb;
    AudioSource sfx;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            transform.DetachChildren();
        }
    }
    private void FixedUpdate()
    {
        //float h = Input.GetAxisRaw("Horizontal");

        float h = speed * Time.fixedDeltaTime;

        //rb.MovePosition(transform.position - transform.up * h);

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 8.25)
        {
            rb.MovePosition(transform.position - transform.up * h);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -8.25)
        {
            rb.MovePosition(transform.position + transform.up * h);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        sfx.Play();
    }
}
