using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveTest : MonoBehaviour
{
    public AnimationCurve curve;

    Vector3 finalPosition;
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        finalPosition = initialPosition + Vector3.forward * 10;
        StartCoroutine(MoveObject());
    }

    void Update()
    {
        
    }

    IEnumerator MoveObject()
    {
        curve.postWrapMode = WrapMode.PingPong;
        yield return new WaitForSeconds(1f);
        float i = 0;
        float rate = 1 / 2f;
        while (i < 4)
        {
            i += rate * Time.deltaTime;
            float eval = curve.Evaluate(i);
            print(eval);
            transform.position = Vector3.Lerp(initialPosition, finalPosition, eval);
            yield return null;
        }
    }
}
