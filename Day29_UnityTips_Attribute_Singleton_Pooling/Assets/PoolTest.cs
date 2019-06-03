using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PoolTest : MonoBehaviour
{
    public EZObjectPool shellPool;

    GameObject shell;

    void Start()
    {
        if (shellPool.TryGetNextObject(Vector3.zero, Quaternion.identity, out shell)) // 가져올 수 있으면 true
        {
            print(shellPool.AvailableObjectCount() == 9);
            print(shellPool.ActiveObjectCount() == 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(shellPool.AvailableObjectCount() == 9);
            print(shellPool.ActiveObjectCount() == 1);
            shell.SetActive(false);
            print(shellPool.AvailableObjectCount() == 10);
            print(shellPool.ActiveObjectCount() == 0);
        }
    }
}
