using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGetAxis : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 120f;

    // Update is called once per frame
    void Update()
    {
        // [-1 ... 1]
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        print(v);

        v *= speed * Time.deltaTime;
        h *= rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, v);
        transform.Rotate(0, h, 0);
    }
}
