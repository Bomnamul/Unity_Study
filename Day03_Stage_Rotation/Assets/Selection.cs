using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public int angle;
    public int duration;
    private bool toggle = false;
    private float time = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (toggle == false)
                toggle = true;
            else
                toggle = false;
        }
        if (toggle == true)
        {
            time += Time.deltaTime;

            if (time < duration)
                transform.Rotate(Vector3.up * angle * Time.deltaTime / duration);
            else
            {
                time = 0;
                toggle = false;
            }
        }
    }
    //private void FixedUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (toggle == false)
    //            toggle = true;
    //        else
    //            toggle = false;
    //    }
    //    if (toggle == true)
    //    {
    //        time += Time.deltaTime;
    //        if (time < duration)
    //            transform.Rotate(Vector3.up * angle * Time.deltaTime / duration);
    //        else
    //        {
    //            time = 0;
    //            toggle = false;
    //        }
    //    }
    //}
}
