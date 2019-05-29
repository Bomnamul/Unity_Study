using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeJump : StateMachineBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 180f;

    PlayerController pc;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseMoveX = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(1))
        {
            pc.moveDirection = (new Vector3(h, 0, v)).normalized;
            float rotationY = mouseMoveX * rotationSpeed * Time.deltaTime;
            pc.transform.Rotate(Vector3.up, rotationY);
        }
        else
        {
            pc.moveDirection = (new Vector3(0, 0, v)).normalized;
            float rotationY = h * rotationSpeed * Time.deltaTime;
            pc.transform.Rotate(Vector3.up, rotationY);
        }

        pc.moveDirection = pc.transform.TransformDirection(pc.moveDirection);
        pc.moveDirection *= moveSpeed;

        // moveDirection 세팅하는 함수 하나만 만들면 코드를 줄일 수 있다.
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
