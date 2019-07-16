using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Slime : MonoBehaviour
{
    AIDestinationSetter destSetter;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        destSetter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destSetter.target == null)
        {
            destSetter.target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }
}
