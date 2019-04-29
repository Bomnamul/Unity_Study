// https://en.wikipedia.org/wiki/Coroutine
// 서브루틴의 일반화
// (입력이 여러개, 출력도 여러개인 함수)

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{

    // 유니티는 단일 스레드만 사용하기 때문에 Coroutine활용이 필요하다.

    // Start is called before the first frame update
    void Start()
    {
        print("Start()");
        StartCoroutine(Todo()); // Todo 함수실행 x callback으로 넘겼다고 봐야 함
        print("B");
    }

    IEnumerator Todo()
    {
        print("A");
        yield return null; // 다음 Update() 호출까지 기다림 (1 frame 기다림)
        print("C");
        yield return new WaitForSeconds(Time.deltaTime); // null과는 살짝 다름
        print("D");
        yield return StartCoroutine(NewTodo()); // yield return 없으면 E가 찍히고 1초 기다리는 동안 G가 먼저 찍힘
        print("G");
    }

    IEnumerator NewTodo()
    {
        print("E");
        yield return new WaitForSeconds(1f);
        print("F");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
