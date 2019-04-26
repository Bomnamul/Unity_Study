using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();         // GetComponent는 cost가 크기 때문에 Start에서 선언
    }
    private void FixedUpdate()                  // Physics관련은 프레임이 가변적이면 결과가 가변으로 나오니 Fixed로 해야 함
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 400f);     // F = mass(질량) * accel(가속도), 400f == 속도의 변화율, 누적되는 함수라 연속 호출하면 안됨
        }
    }
}
