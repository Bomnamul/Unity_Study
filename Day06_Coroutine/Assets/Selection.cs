using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public float angle = 90f;
    public float duration = 1f;

    bool isRotating = false;
    float remainingAngle;
    float remainingDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            isRotating = true;
            remainingAngle = angle;
            remainingDuration = duration;
            StartCoroutine(Rotating());
        }
    }

    IEnumerator Rotating()
    {
        float anglePerFrame = (remainingAngle / remainingDuration) * Time.deltaTime;
        while (true)
        {
            if (remainingAngle < anglePerFrame)
            {
                anglePerFrame = remainingAngle;
                isRotating = false;
            }
            transform.Rotate(Vector3.up * anglePerFrame);
            remainingAngle -= anglePerFrame;
            remainingDuration -= Time.deltaTime;
            yield return null;
        }
    }
}
