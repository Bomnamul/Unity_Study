using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float speed = 500f;
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir2 = new Vector3(v, 0, 0);
        dir2 = dir2.normalized;
        transform.Rotate(dir2 * speed * Time.deltaTime);
    }
}
