using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChase : StateMachineBehaviour
{
    public float chaseSpeed = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform target = animator.GetComponent<MobController>().player;
        Vector3 dir = target.position - animator.transform.position;
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation,
                                                       Quaternion.LookRotation(dir),
                                                       0.1f);
        if (dir.magnitude > 1f)
        {
            animator.transform.Translate(0f, 0f, chaseSpeed * Time.deltaTime);
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
