using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public int angle;
    public int duration;
    private bool toggle = false;
    public float restAngle;
    public float restDuration;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (toggle == false)
                toggle = true;
            else
                toggle = false;
            restAngle = angle;
            restDuration = duration;
        }
        if (toggle == true)
        {
            transform.Rotate(Vector3.up * restAngle / restDuration * Time.deltaTime);
            restAngle -= restAngle / restDuration * Time.deltaTime;
            restDuration -= Time.deltaTime;
            if (restAngle < 0 || restDuration < 0)
            {
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
