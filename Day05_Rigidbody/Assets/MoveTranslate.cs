using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTranslate : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 120f;

    // Update is called once per frame
    void Update()
    {
        // [-1, 0, 1]
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        v *= speed * Time.deltaTime;
        h *= rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, v);
        transform.Rotate(0, h, 0);
    }
}
