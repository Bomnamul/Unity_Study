using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipingAttack : StateMachineBehaviour, IHitBoxResponder
{
    //Transform axe;

    public int damage = 20;
    public bool enabledMultipleHits = false;

    HitBox hitBox;
    Dictionary<int, int> hitObjects;

    //HitBox hitBox;
    //bool isDamaged = false;

    public void CollisionWith(Collider collider, HitBox hitBox)
    {
        HurtBox hurtBox = collider.GetComponent<HurtBox>();

        hurtBox.GetHitby(damage); // debugging
        //collider.GetComponentInParent<Health>().DecreaseHP(damage);

        Vector3 cameraTargetPosition = hitBox.transform.root.position;
        Vector3 hitPoint;
        Vector3 hitNormal;
        Vector3 hitDirection;

        hitBox.GetContactInfo(from: cameraTargetPosition,
                              to: collider.transform.position,
                              out hitPoint, out hitNormal, out hitDirection,
                              2f);

        BoxHitReaction hr = collider.GetComponentInParent<BoxHitReaction>();
        hr?.Hurt(damage, hitPoint, hitNormal, hitDirection);

        //if (isDamaged == false)
        //{
        //    isDamaged = true;
        //    Debug.Log("Collision With: " + collider);
        //    collider.GetComponentInParent<MobStatus>().GetDamaged(25f);
        //}
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox = animator.GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
        hitBox.enabledMultipleHits = this.enabledMultipleHits;
        hitBox.StartCheckingCollision();
        hitObjects = new Dictionary<int, int>();

        //isDamaged = false;
        //axe = animator.GetComponent<PlayerController>().weaponHolder.GetChild(0);
        //hitBox = axe.GetComponentInChildren<HitBox>();
        //hitBox.SetResponder(this);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (0.55f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.6f && !animator.IsInTransition(0))
        {
            hitBox.UpdateHitBox();
        }

        if (0.6f < stateInfo.normalizedTime && !animator.IsInTransition(0))
        {
            hitBox.StopCheckingCollision();
        }

        //if (stateInfo.normalizedTime > 0.35f)
        //{
        //    hitBox.StartCheckingCollision();
        //}

        //if (stateInfo.normalizedTime > 0.5f)
        //{
        //    hitBox.StopCheckingCollision();
        //}

        //hitBox.UpdateHitBox();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
