﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    public float moveSpeed = 4;
    //public bool onPortal = false;

    public enum State { Entry = -1, Idle, Walk, Attack, HammerAttack }
    public State state = State.Idle;
    public State prevState = State.Entry;

    Animator anim;
    float lastX, lastY;

    public void SetState(State state)
    {
        prevState = this.state;
        this.state = state;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(this);
    }

    //IEnumerator Start()
    //{
    //    while(true) // yield return null로 한 프레임 단위로 반복하게 바꿔보자
    //    {
    //        anim.SetInteger("State", (int)state);
    //        anim.SetInteger("PrevState", (int)prevState);

    //        switch (state) // default는 안쓰는 것이 MS의 권고사항
    //        {
    //            case State.Idle:
    //            case State.Walk:
    //                yield return StartCoroutine(Move());
    //                break;
    //            case State.Attack:
    //                yield return new WaitForSeconds(0.417f);
    //                SetState(State.Idle);
    //                break;
    //            case State.HammerAttack:
    //                yield return new WaitForSeconds(0.333f);
    //                SetState(State.Idle);
    //                break;
    //        }
    //    }
    //}

    //IEnumerator Move()
    //{
    //    while(true)
    //    {
    //        float h = Input.GetAxisRaw("Horizontal");
    //        float v = Input.GetAxisRaw("Vertical");
    //        Vector3 heading = new Vector3(h, v, 0).normalized;
    //        Vector3 movement = heading * moveSpeed * Time.deltaTime;
    //        transform.position += movement;

    //        UpdateAnimation(heading);

    //        if (Input.GetKeyDown(KeyCode.X))
    //        {
    //            anim.SetTrigger("OnAttack");
    //            SetState(State.Attack);
    //            break;
    //        }

    //        if (Input.GetKeyDown(KeyCode.C))
    //        {
    //            anim.SetTrigger("OnHammerAttack");
    //            SetState(State.HammerAttack);
    //            break;
    //        }

    //        anim.SetInteger("State", (int)state);
    //        anim.SetInteger("PrevState", (int)prevState);

    //        yield return null;
    //    }
    //}

    IEnumerator Start()
    {
        while(true)
        {
            anim.SetInteger("State", (int)state);
            anim.SetInteger("PrevState", (int)prevState);

            switch (state)
            {
                case State.Idle:
                case State.Walk:
                    Move();
                    break;
                case State.Attack:
                    yield return new WaitForSeconds(0.417f);
                    SetState(State.Idle);
                    break;
                case State.HammerAttack:
                    yield return new WaitForSeconds(0.583f);
                    SetState(State.Idle);
                    break;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                UIController.instance.bag.OnOff();
            }

            yield return null;
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 heading = new Vector3(h, v, 0).normalized;
        Vector3 movement = heading * moveSpeed * Time.deltaTime;

        //if (!onPortal)
        //{
            transform.position += movement;

            UpdateAnimation(heading);
        //}

        if (h != 0 || v != 0)
        {
            SetState(State.Walk);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("OnAttack");
            SetState(State.Attack);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("OnHammerAttack");
            SetState(State.HammerAttack);
        }
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if (heading.x == 0 && heading.y == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("OnMove", false);
            SetState(State.Idle);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetFloat("DirX", heading.x);
            anim.SetFloat("DirY", heading.y);
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("OnMove", true);
            SetState(State.Walk);
        }
    }
}