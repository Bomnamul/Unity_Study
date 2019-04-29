using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn() // Console에서 코딩하는 것과 매우 비슷 (사용하기 편하다)
    {
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return null;
        }
    }
}
