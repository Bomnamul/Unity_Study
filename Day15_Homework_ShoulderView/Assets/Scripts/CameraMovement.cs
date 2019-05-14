using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 target;
    Vector3 zoom;
    float limit = -5;
    float mouseSensitivityX = 2f;
    float mouseSensitivityY = 2f;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent.position - transform.position;
        transform.LookAt(transform.position + target);
    }

    // Update is called once per frame
    void Update()
    {
        limit = Mathf.Clamp(limit, -10, -2);
        zoom = new Vector3(0f, 0f, limit);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            transform.Translate(0f, 0f, 1f);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            transform.Translate(0f, 0f, -1f);
        }

        if (Input.GetMouseButton(0))
        {
            transform.parent.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX, Space.World);
            transform.parent.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensitivityY, Space.Self);
        }

        if (Input.GetMouseButton(1))
        {
            GameObject.Find("BoxBoy").transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
            transform.parent.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensitivityY, Space.Self);
        }
    }
}
