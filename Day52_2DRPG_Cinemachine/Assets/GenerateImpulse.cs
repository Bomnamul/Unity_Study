using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GenerateImpulse : MonoBehaviour
{
    void Start()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }
}
