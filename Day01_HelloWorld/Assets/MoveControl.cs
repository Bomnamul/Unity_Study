using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()   // Update의 빈도는 Frame, 한 Frame의 변화량을 기술
    {
        float speed = 10f; // 10m/s
        float h = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        float v = Input.GetAxisRaw("Vertical");   // same

        // 1:
        //h *= speed * Time.deltaTime;              // Time.deltaTime (이전(n - 1) 프레임의 렌더링 시간)
        //v *= speed * Time.deltaTime;

        // Box기준으로 이동 (Local)
        //transform.Translate(Vector3.right * h);   // Translate: move, Vector3.right: (1, 0, 0)
        //transform.Translate(Vector3.forward * v); // Vector3.forward: (0, 0, 1)

        // 2:
        Vector3 dir = new Vector3(0, 0, v);
        dir = dir.normalized;                       // length == 1
        transform.Translate(dir * speed * Time.deltaTime);  // transform.Forward(전방 벡터(local z축)가 무엇인가, 현재 오브젝트가 바라보고 있는 방향), Vector3.Forward와는 다르다

        Vector3 dir2 = new Vector3(0, h, 0);
        dir2 = dir2.normalized;
        transform.Rotate(dir2 * 50 * speed * Time.deltaTime);   // 헷갈릴 수 있으니 잘 알아둘 것
    }
}
