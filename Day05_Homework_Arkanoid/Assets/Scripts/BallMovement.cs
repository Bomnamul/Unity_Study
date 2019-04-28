using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private float speed = 700f;
    private bool ready = true;

    Rigidbody rb;
    Vector3 dir = new Vector3(1, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && ready == true)
        {
            rb.isKinematic = false;
            rb.AddForce(dir * speed);
            ready = false;
        }
    }
}
