// https://docs.unity3d.com/ScriptReference/Quaternion.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 50f, 0f);
        print(Mathf.Approximately(transform.eulerAngles.y, 50f));

        //transform.rotation *= Quaternion.Euler(0f, 100f, 0f);         // Quaternion의 곱셈 == 현재 rotation 정보의 값에 누적시킴 (단순 곱셈 X)
        transform.rotation = Quaternion.Euler(0f, 100f, 0f);
        print(Mathf.Approximately(transform.eulerAngles.y, 100f));
        transform.Rotate(Vector3.up * 90f);
        print(Mathf.Approximately(transform.eulerAngles.y, 190f));      // Inspector에선 -170으로 표기
        transform.rotation *= Quaternion.AngleAxis(90f, Vector3.up);
        print(Mathf.Approximately(transform.eulerAngles.y, 280f));      // -80

        transform.rotation = Quaternion.identity;                       // 단위 행렬과 비슷 (초기값)
        print(Mathf.Approximately(transform.eulerAngles.x, 0f));
        print(Mathf.Approximately(transform.eulerAngles.y, 0f));
        print(Mathf.Approximately(transform.eulerAngles.z, 0f));
        transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
        print(Mathf.Approximately(transform.eulerAngles.x, 90f));

        transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up); // forward를 right벡터로 up기준으로 돌림
        print(Mathf.Approximately(transform.eulerAngles.y, 90f));
        transform.rotation = Quaternion.LookRotation(Vector3.right);
        print(Mathf.Approximately(transform.eulerAngles.y, 90f));

        Quaternion q1 = Quaternion.Euler(0f, -45f, 0f);
        Quaternion q2 = Quaternion.Euler(0f, 45f, 0f);
        print(Mathf.Approximately(Quaternion.Angle(q1, q2), 90f));
    }
}
