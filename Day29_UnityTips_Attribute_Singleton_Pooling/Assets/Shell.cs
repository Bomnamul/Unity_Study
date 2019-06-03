using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private void OnEnable() // 초기화 작업 (ex: rigidbody에서 velocity 값을 초기화 시켜줘야 함)
    {
        print("OnEnable");
    }

    private void OnDisable() // 후처리 작업
    {
        print("OnDisable");
    }
}
