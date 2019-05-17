using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeLocomotionBT : StateMachineBehaviour
{
    public float moveSpeed = 4f;

    PlayerController pc;
    Transform weaponHolder;
    Transform backHolder;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
        pc.moveSpeed = moveSpeed;
        weaponHolder = pc.weaponHolder;
        backHolder = pc.backHolder;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.FrameMove();
        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            weaponHolder.GetChild(0).GetComponent<Collider>().enabled = true;
            weaponHolder.DetachChildren();
            animator.SetBool("HaveWeapon", false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("ArmDisarm");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
