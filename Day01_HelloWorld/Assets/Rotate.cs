using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) // D가 눌러지면 (true, false)
            transform.Rotate(Vector3.up * 5f);
    }
}
