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
    private void FixedUpdate() // update로 해보자, player의 rigidbody가 필요한가?
    {
        //float h = Input.GetAxisRaw("Horizontal");

        float h = speed * Time.fixedDeltaTime;

        //rb.MovePosition(transform.position - transform.up * h);

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 8.25) // Mathf.Clamp(pos.x, -3.1f, 3.1f) 써볼 것
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
