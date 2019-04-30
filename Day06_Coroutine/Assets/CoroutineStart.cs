using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStart : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start() // Start()도 Coroutine이 될 수 있다.
    {
        print("Start()");
        StartCoroutine(Todo());
        print("B");
        yield return null;
        print("D");
    }
    IEnumerator Todo()
    {
        print("A");
        yield return null;
        print("C");
    }

    // Update is called once per frame // Update 함수는 Coroutine이 될 수 없다(Update는 매 프레임마다 호출되기에 멈출 수 없다)
    // Runtime Error ! : Script error (CoroutineStart): Update() can not be a coroutine.
    //IEnumerator Update()
    //{
    //    yield return null;
    //}
}
