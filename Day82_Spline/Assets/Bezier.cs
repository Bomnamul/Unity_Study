using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public Transform P0;
    public Transform P1;
    public Transform P2;
    public Transform P3;

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
            //Vector3 c = QuadraticBezier(t, P0.position, P1.position, P2.position);
            Vector3 c = CubicBezier(t, P0.position, P1.position, P2.position, P3.position);
            line.SetPosition(i, c);
        }
    }

    private Vector3 CubicBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float invt = 1 - t;
        float invtt = invt * invt;
        float invttt = invtt * invt;
        return (invttt * p0) + (3 * invtt * t * p1) + (3 * invt * t * t * p2) + (t * t * t * p3);
    }

    private Vector3 QuadraticBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float invt = 1 - t;
        return (invt * invt * p0) + (2 * invt * t * p1) + (t * t * p2);
    }
}
