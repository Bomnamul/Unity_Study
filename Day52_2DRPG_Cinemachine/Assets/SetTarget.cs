using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetTarget : MonoBehaviour
{
    CinemachineTargetGroup targetGroup;
 
    void Start()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
        if (targetGroup != null)
        {
            var player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player != null && targetGroup.FindMember(player) == -1)
            {
                targetGroup.AddMember(player, 1f, 0.5f);
            }
        }
    }
}
