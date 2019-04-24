using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Vector3: struct type (value), class와는 다르다 (instance는 ref)
        Vector3 v = new Vector3(1f, 1f, 1f);
        Vector3 u = new Vector3(1f, 1f, 1f);
        print(v == u);
        print(2 * v == new Vector3(2f, 2f, 2f));
        v = new Vector3(1f, 0f, 0f);
        print(v == Vector3.right);
        print(v.magnitude == 1f);
        print(Vector3.one.magnitude); // Sqrt(3)
        print(v.ToString() == "1.0, 0.0, 0.0");
        print(v);
        print(Vector3.Distance(Vector3.zero, u) == u.magnitude); // 기본적으로 Vector는 방향과 길이를 의미하지만 이 경우는 좌표 점으로 해석하는 게 좋다
        print(Vector3.Angle(Vector3.right, Vector3.up) == 90f);  // 맥락에 따라 해석을 잘 해줘야 함
        print(Vector3.Angle(Vector3.right, Vector3.forward) == 90f);
        print((2 * Vector3.up).normalized == Vector3.up);
        print((2 * Vector3.up) / (2 * Vector3.up).magnitude == Vector3.up);
        print((2 * Vector3.up) / (2 * Vector3.up).magnitude == (2 * Vector3.up).normalized);
        print(Vector3.Cross(Vector3.right, Vector3.up) == Vector3.forward); // 왼손 좌표계 기준(순서가 중요)
        print(Vector3.Cross(Vector3.up, Vector3.right) == -Vector3.forward);
        print(Vector3.Dot(Vector3.up, Vector3.up) == 1f);
        print(Vector3.Dot(Vector3.right, Vector3.up) == 0f); // Cos90 == 0, Dot == |A||B|cos == cos, 코사인 세타 (프로젝션) 기하학적으로 그려보면 이해하기 쉬울지도...

        transform.position = new Vector3(5f, 0, 0); // 좌표값으로 해석(해당 위치로 벡터를 이동)
    }
}
