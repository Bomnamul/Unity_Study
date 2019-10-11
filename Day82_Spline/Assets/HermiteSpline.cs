using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteSpline : MonoBehaviour
{
    public Transform P0;
    public Transform M0;
    public Transform P1;
    public Transform M1;

    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startColor = Color.yellow;
        line.endColor = Color.green;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
    }

    void Update()
    {
        DrawCurve();
    }

    private void DrawCurve()
    {
        line.positionCount = 20;
        for (int i = 0; i < line.positionCount; i++)
        {
            float t = i / ((float)line.positionCount - 1);
            Vector3 c = Hermite(t, P0.position, 3 * (M0.position - P0.position), P1.position, 3 * (M1.position - P1.position));
            line.SetPosition(i, c);
        }
    }

    private Vector3 Hermite(float t, Vector3 p0, Vector3 m0, Vector3 p1, Vector3 m1)
    {
        float tt = t * t;
        float ttt = tt * t;
        return (2 * ttt - 3 * tt + 1) * p0 + (ttt - 2 * tt + t) * m0 + (-2 * ttt + 3 * tt) * p1 + (ttt - tt) * m1;
    }
}
