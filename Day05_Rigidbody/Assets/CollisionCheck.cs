using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    // **충돌은 쌍방이다 (가해자, 피해자가 항상 존재)

    // OnCollisionX() 발생 조건
    //
    // 1. 두 개의 gameObject 모두 collider component가 존재해야 함
    // 2. 둘 중 하나는 rigidbody component 가 있어야 함
    // 3. rigidbody를 가진 gameObject가 움직여 충돌되었을 때 발생, 그 반대는 발생하지 않음(예측 불가)


    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter: " + collision.gameObject.name); // plane
        foreach (ContactPoint contact in collision.contacts)     // contacts(접촉면)을 array형태로 가지고 있음
        {
            Debug.DrawRay(contact.point, contact.normal, Color.magenta, 5f); // normal vector == 직교
        }
    }

    private void OnCollisionStay(Collision collision) // 충돌 이후 Action이 있음
    {
        print("OnCollisionStay: " + collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        print("OnCollisionExit: " + collision.gameObject.name);
    }

    // OnTriggerEnter 발생 조건
    //
    // 1. 두 개의 gameObject 모두 collider component가 존재해야 한다.
    // 2. 둘 중 하나는 rigidbody component가 있어야 한다.
    // 3. 둘 중 하나는 collider component에 Is Trigger 가 ON 되어야 한다.
    // 4. 그리고 어느 쪽이 움직이더라도 서로 만나면 이벤트가 발생한다.
    // 5. OnTriggerEnter 발생 시 OnCollisionEnter가 발생하지 않는다.

    private void OnTriggerEnter(Collider other) // 충돌을 Check but 아무런 Action 없음
    {
        print("OnTriggerEnter: " + other.gameObject.name);
    }

    private void OnTriggerStay(Collider other)
    {
        print("OnTriggerStay: " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit: " + other.gameObject.name);
    }
}
