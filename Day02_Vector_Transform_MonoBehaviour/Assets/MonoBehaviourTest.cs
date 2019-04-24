// Unity Class Diagram: https://cdn-ak.f.st-hatena.com/images/fotolife/g/glass-_-onion/20160614/20160614095059.png
// http://glassonion.hatenablog.com/entry/2016/06/13/173933
// Scripting Tips: https://kimseunghyun76.tistory.com/194
// Object: https://docs.unity3d.com/ScriptReference/Object.html
//  name, Instantiate(), Destroy(), FindObjectOfType()
// GameObject: https://docs.unity3d.com/ScriptReference/GameObject.html
//  transform, tag, GetComponent(), Component 관리
// Component: https://docs.unity3d.com/ScriptReference/Component.html
//  Object에서 상속됨, gameObject, transform, GetComponent(), 
// Behaviour: https://docs.unity3d.com/ScriptReference/Behaviour.html
//  component의 enable/disable 기능
// GameObject는 transform 을 하나 가지고 있고 삭제할 수 없다.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourTest : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed = 1000; // 적용 안됨
    private void Awake()
    {
        print("Awake()");
    }

    // Start is called before the first frame update
    void Start()
    {
        print("Start()");
        print(name == "SomeTest");
        print(name == gameObject.name);
        print(transform == GetComponent<Transform>());
        print(transform == gameObject.GetComponent<Transform>());
        print(GetComponent<MonoBehaviourTest>() == this);
    }

    // Update is called once per frame
    void Update()
    {
        print("Update()");
    }

    private void FixedUpdate()
    {
        print("FixedUpdate()");
    }

    private void LateUpdate()
    {
        print("LateUpdate()");
    }
}
