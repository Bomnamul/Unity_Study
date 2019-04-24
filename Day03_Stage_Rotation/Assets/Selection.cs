using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public int angle;
    public int duration;
    private int toggle = 0;
    private float time = 0;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (toggle == 0)
                toggle = 1;
            else
                toggle = 0;
        }
        if (toggle == 1)
        {
            time += Time.deltaTime;
            if (time < duration)
                transform.Rotate(Vector3.up * angle * Time.deltaTime / duration);
            else
            {
                time = 0;
                toggle = 0;
            }
        }
    }
}
