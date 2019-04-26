using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool ready = true;

    Rigidbody rb;
    Vector3 dir = new Vector3(1, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ready == true)
        {
            rb.isKinematic = false;
            rb.AddForce(dir * 1000f);
            ready = false;
        }
    }
}
