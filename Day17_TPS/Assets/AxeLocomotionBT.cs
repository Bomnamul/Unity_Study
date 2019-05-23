using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeLocomotionBT : StateMachineBehaviour
{
    public float moveSpeed = 3f;
    PlayerController pc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
        pc.moveSpeed = moveSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.FrameMove();

        if (Input.GetKeyDown(KeyCode.C) && !animator.IsInTransition(0))
        {
            animator.SetTrigger("ComboAttack");
        }

        if (Input.GetKeyDown(KeyCode.V) && !animator.IsInTransition(0))
        {
            animator.SetTrigger("Attack2");
        }

        if (Input.GetKeyDown(KeyCode.E) && pc.isEquipped && !animator.IsInTransition(0))
        {
            animator.SetTrigger("DropWeapon");
        }

        if (Input.GetKeyDown(KeyCode.X) && !pc.isDisarmed && pc.isEquipped && !animator.IsInTransition(0))
        {
            animator.SetTrigger("Disarm");
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
