using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    private float h;
    private float v;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        var dirH = new Vector3(h, 0f, 0f);
        var dirV = new Vector3(0f, 0f, v);

        rb.transform.Translate(dirH * speed * Time.deltaTime);
        rb.transform.Translate(dirV * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 300f);
        }
    }
}
