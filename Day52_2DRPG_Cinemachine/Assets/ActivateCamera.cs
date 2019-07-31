﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActivateCamera : MonoBehaviour
{
    public float distance = 5.5f;
    CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (vcam != null && vcam.Follow != null && GameFlow.instance.player != null)
        {
            var bb = vcam.Follow.GetComponent<CinemachineTargetGroup>().Sphere;
            //print(bb.extents + ", " + bb.extents.magnitude);
            print(bb.radius * 2 + ", " + distance);
            if (bb.radius * 2 >= distance)
            {
                vcam.Priority = 9;
            }
            else
            {
                vcam.Priority = 11;
            }
        }
    }
}