using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWeaponLocomotionBT : StateMachineBehaviour
{
    public float moveSpeed = 4f;
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

        if (Input.GetKeyDown(KeyCode.E) && !pc.isEquipped && !animator.IsInTransition(0)) // !animator.IsInTransition: Transition 구간에선 key 입력을 안 받음
        {
            GameObject weapon = pc.GetNearestWeaponIn(1.5f, 180f, "RightWeapon");
            if (weapon == null)
            {
                Debug.Log("No weapon");
                return;
            }

            animator.SetInteger("HandlingWeaponID", weapon.GetComponent<WeaponType>().weaponId);
            animator.SetTrigger("PickupWeapon");
        }

        if (Input.GetKeyDown(KeyCode.X) && pc.isDisarmed && !pc.isEquipped && !animator.IsInTransition(0))
        {
            Transform weaponDisarmHolder = animator.GetComponent<PlayerController>().weaponDisarmHolder;
            Transform weapon = weaponDisarmHolder.GetChild(0);
            animator.SetInteger("HandlingWeaponID", weapon.GetComponent<WeaponType>().weaponId);
            animator.SetTrigger("Equip");
        }

        if (Input.GetKeyDown(KeyCode.V) && pc.isDisarmed && !pc.isEquipped && !animator.IsInTransition(0))
        {
            Transform weaponDisarmHolder = animator.GetComponent<PlayerController>().weaponDisarmHolder;
            Transform weapon = weaponDisarmHolder.GetChild(0);
            animator.SetInteger("HandlingWeaponID", weapon.GetComponent<WeaponType>().weaponId);
            animator.SetTrigger("DrawAttack");
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
