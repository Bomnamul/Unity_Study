using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadCube : MonoBehaviour
{
    //public GameObject cubePrefab;
    public AssetReference cubePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(cubePrefab);
            StartCoroutine(LoadCubePrefab());
        }
    }

    IEnumerator LoadCubePrefab()
    {
        yield return cubePrefab.InstantiateAsync(Vector3.zero, Quaternion.identity);
    }
}
