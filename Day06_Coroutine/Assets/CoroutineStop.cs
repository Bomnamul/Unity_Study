// http://theeye.pe.kr/archives/2725
// https://kimseunghyun76.tistory.com/304

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStop : MonoBehaviour
{
    // 1:

    //IEnumerator Start()
    //{
    //    yield return StartCoroutine("Todo", 2f); // 함수 이름을 문자열로 받을 수 있다(Invoke in MonoBehaviour)
    //}

    //IEnumerator Todo(float someParameter)
    //{
    //    while (true)
    //    {
    //        print("someParameter: " + someParameter);
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StopCoroutine("Todo"); // StartCoroutine이 string으로 호출된 경우에만 가능(항상 쌍으로 돌아야 함)
    //    }
    //}

    // 2:

    //IEnumerator co;

    //void Start()
    //{
    //    co = Todo(2f, "string");
    //    StartCoroutine(co);
    //}

    //IEnumerator Todo(float someParameter, string str)
    //{
    //    while (true)
    //    {
    //        print("someParameter: " + someParameter + str);
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StopCoroutine(co); // StartCoroutine과 항상 쌍으로 돌아야 함
    //    }
    //}

    // 3:

    Coroutine co, co2;

    void Start()
    {
        co = StartCoroutine(Todo(2f, "string"));
        co2 = StartCoroutine(Todo(2f, "string2"));
    }

    IEnumerator Todo(float someParameter, string str)
    {
        while (true)
        {
            print("someParameter: " + someParameter + str);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StopCoroutine(co); // StartCoroutine과 항상 쌍으로 돌아야 함
            StopAllCoroutines();
        }
    }
}
