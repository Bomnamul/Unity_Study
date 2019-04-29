using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public bool Check = false;
    float speed = 20f;
    float power = 200f;
    float ppower;
    float p2power;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ppower = power;
        p2power = power;
    }

    // Update is called once per frame
    void Update()
    {
        if (Check)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.transform.Rotate(-Vector3.up * speed * 10 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.transform.Rotate(Vector3.up * speed * 10 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.F))
            {
                if (ppower < 20000f)
                {
                    ppower += 1000f;
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                rb.AddForce(transform.forward * ppower);
                ppower = power;
            }
        }
        
        if (!Check)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.transform.Rotate(-Vector3.up * speed * 10 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.transform.Rotate(Vector3.up * speed * 10 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad0))
            {
                if (p2power < 20000f)
                {
                    p2power += 1000f;
                }
            }
            if (Input.GetKeyUp(KeyCode.Keypad0))
            {
                rb.AddForce(transform.forward * p2power);
                p2power = power;
            }
        }
    }
}
