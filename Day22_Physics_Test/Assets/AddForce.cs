using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public int forceMode;
    public float power;

    Vector3 force;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        force = new Vector3(0f, 0f, power);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            switch (forceMode)
            {
                case 0:
                    rb.AddForce(force, ForceMode.Force);
                    break;
                case 1:
                    rb.AddForce(force, ForceMode.Acceleration);
                    break;
                case 2:
                    rb.AddForce(force, ForceMode.Impulse);
                    break;
                case 3:
                    rb.AddForce(force, ForceMode.VelocityChange);
                    break;
            }
        }
    }
}
