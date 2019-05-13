using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform holder;

    Camera fpsCamera;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>(); // DFS로 찾을 것이라 Main camera를 먼저 발견
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 4f))
            {
                print(hit.transform.name);
                var item = hit.transform.GetComponent<Pickable>();
                if (item != null)
                {
                    Pickup(hit.transform);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ThrowItem();
        }
    }

    private void ThrowItem()
    {
        if (holder.childCount == 1)
        {
            Transform item = holder.GetChild(0);
            item.SetParent(null);
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.forward * 700f);
        }
    }

    private void Pickup(Transform item)
    {
        if (holder.childCount == 0)
        {
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.SetParent(holder); // item.parent = ... 보다 권장
            //item.transform.position = holder.transform.position;
            item.transform.localPosition = Vector3.zero;
        }
    }
}
