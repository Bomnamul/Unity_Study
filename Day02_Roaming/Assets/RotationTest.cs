using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public Transform wayRoot;
    int a = 1;
    // Update is called once per frame
    void Update()
    {
        var list = new List<Transform>();
        for (int i = 0; i < wayRoot.childCount; i++)
        {
            list.Add(wayRoot.GetChild(i));
        }

        transform.LookAt(list[a]);
        if (Vector3.Distance(transform.parent.position, list[a].position) < 0.3)
        {
            if (a == list.Count - 1)
            {
                a = 0;
            }
            else
            {
                a++;
            }
        }
    }
}
