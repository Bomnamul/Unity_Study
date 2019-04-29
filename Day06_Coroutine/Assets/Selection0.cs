using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection0 : MonoBehaviour
{
    public float angle = 90f;
    public float duration = 1f;

    bool isRotating = false;
    float remainingAngle;
    float remainingDuration;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            isRotating = true;
            remainingAngle = angle;
            remainingDuration = duration;
        }
        if (isRotating)
        {
            float anglePerFrame = (remainingAngle / remainingDuration) * Time.deltaTime;
            if (remainingAngle < anglePerFrame)
            {
                anglePerFrame = remainingAngle;
                isRotating = false;
            }
            transform.Rotate(Vector3.up * anglePerFrame);
            remainingAngle -= anglePerFrame;
            remainingDuration -= Time.deltaTime;
        }
    }
}
