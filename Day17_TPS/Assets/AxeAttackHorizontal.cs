using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttackHorizontal : StateMachineBehaviour, IHitBoxResponder
{
    Transform axe;
    HitBox hitBox;
    bool isDamaged = false;

    public void CollisionWith(Collider collider)
    {
        if (isDamaged == false)
        {
            isDamaged = true;
            Debug.Log("Collision With: " + collider);
            collider.GetComponentInParent<MobStatus>().GetDamaged(25f);
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isDamaged = false;
        axe = animator.GetComponent<PlayerController>().weaponHolder.GetChild(0);
        hitBox = axe.GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.35f)
        {
            hitBox.StartCheckingCollision();
        }

        if (stateInfo.normalizedTime > 0.5f)
        {
            hitBox.StopCheckingCollision();
        }

        hitBox.UpdateHitBox();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox.StopCheckingCollision();
    }

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
