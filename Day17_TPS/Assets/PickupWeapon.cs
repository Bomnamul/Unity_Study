using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : StateMachineBehaviour
{
    PlayerController pc;
    Transform weaponHolder;
    Transform bowHolder;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
        weaponHolder = pc.weaponHolder;
        bowHolder = pc.bowHolder;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (weaponHolder.childCount == 0 && stateInfo.normalizedTime > 0.22f)
        {
            GameObject weapon = pc.GetNearestWeaponIn(radius: 1.5f, angle: 180f, weaponTag: "RightWeapon");
            if (weapon == null)
                return;
            weapon.GetComponent<Rigidbody>().isKinematic = true;
            Collider[] colliders = weapon.GetComponents<Collider>();
            foreach (var c in colliders)
                c.enabled = false;
            weapon.transform.SetParent(weaponHolder);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            animator.SetInteger("HoldingWeaponID", weapon.GetComponent<WeaponType>().weaponId);
        }

        if (bowHolder.childCount == 0 && stateInfo.normalizedTime > 0.22f)
        {
            GameObject weapon = pc.GetNearestWeaponIn(radius: 1.5f, angle: 180f, weaponTag: "LeftWeapon");
            if (weapon == null)
                return;
            weapon.GetComponent<Rigidbody>().isKinematic = true;
            Collider[] colliders = weapon.GetComponents<Collider>();
            foreach (var c in colliders)
                c.enabled = false;
            weapon.transform.SetParent(bowHolder);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            animator.SetInteger("HoldingWeaponID", weapon.GetComponent<WeaponType>().weaponId);
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
