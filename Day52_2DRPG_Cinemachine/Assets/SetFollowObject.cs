using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetFollowObject : MonoBehaviour
{
    CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (vcam != null && vcam.Follow == null)
        {
            vcam.Follow = GameObject.FindGameObjectWithTag("Player")?.transform;
            var confiner = vcam.GetComponent<CinemachineConfiner>();
            confiner.m_BoundingShape2D = GameObject.Find("CameraConfiner")?.GetComponent<PolygonCollider2D>();
        }
    }
}
