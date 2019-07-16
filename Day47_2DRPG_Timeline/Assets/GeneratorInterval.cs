using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorInterval : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 2f;
    public float destroyDelay = 3f;

    void Start()
    {
        StartCoroutine(GenerateObject(interval));
    }

    IEnumerator GenerateObject(float interval)
    {
        yield return new WaitForSeconds(interval);
        var o = Instantiate(prefab, transform.position, transform.rotation);
        Destroy(o, destroyDelay);
        StartCoroutine(GenerateObject(interval));
    }
}
