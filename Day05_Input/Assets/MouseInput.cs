using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public GameObject sphere;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    print("mousePosition: " + Input.mousePosition);
        //    print("Pressed left click");
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    print("Pressed left click: Down");
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    print("Pressed left click: Up");
        //}
        //if (Input.GetMouseButton(1))
        //{
        //    print("Pressed right click");
        //}
        //if (Input.GetMouseButton(2))
        //{
        //    print("Pressed wheel click");
        //}

        if (Input.GetButtonDown("Fire1"))
        {
            print("Fire1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var o = Instantiate(sphere, hit.point, Quaternion.identity);
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
                Destroy(o, 2f);
            }
        }
    }
}
