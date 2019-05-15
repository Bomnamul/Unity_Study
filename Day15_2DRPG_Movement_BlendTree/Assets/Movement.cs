using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 4;

    Animator anim;
    float lastX, lastY;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 heading = new Vector3(h, v, 0).normalized;
        Vector3 movement = heading * moveSpeed * Time.deltaTime;
        transform.position += movement;

        UpdateAnimation(heading);

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetTrigger("OnAttack");
        }
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if (heading.x == 0 && heading.y == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("Movement", false);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetBool("Movement", true);
        }
        anim.SetFloat("DirX", heading.x);
        anim.SetFloat("DirY", heading.y);
    }
}
