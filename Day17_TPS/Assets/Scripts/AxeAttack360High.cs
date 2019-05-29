using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack360High : StateMachineBehaviour, IHitBoxResponder
{
    public int damage = 5;
    public bool enabledMultipleHits = false;

    HitBox hitBox;
    bool entered;

    //HitBox hitBox;
    //bool isDamaged = false;

    public void CollisionWith(Collider collider, HitBox hitBox)
    {
        HurtBox hurtBox = collider.GetComponent<HurtBox>();

        hurtBox.GetHitby(damage); // debugging
        //collider.GetComponentInParent<Health>().DecreaseHP(damage);

        Vector3 cameraTargetPosition = hitBox.transform.root.Find("CameraTarget").transform.position;
        Vector3 hitPoint;
        Vector3 hitNormal;
        Vector3 hitDirection;

        hitBox.GetContactInfo(from: cameraTargetPosition,
                              to: collider.transform.root.transform.position,
                              out hitPoint, out hitNormal, out hitDirection,
                              2f);

        BoxHitReaction hr = collider.GetComponentInParent<BoxHitReaction>();
        hr?.Hurt(damage, hitPoint, hitNormal, hitDirection, ReactionType.Head);

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
        hitBox = animator.GetComponent<PlayerController>().weaponHolder.GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
        hitBox.enabledMultipleHits = this.enabledMultipleHits;
        hitBox.StartCheckingCollision();
        entered = false;

        //isDamaged = false;
        //axe = animator.GetComponent<PlayerController>().weaponHolder.GetChild(0);
        //hitBox = axe.GetComponentInChildren<HitBox>();
        //hitBox.SetResponder(this);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (0.15f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.45f)
        {
            hitBox.UpdateHitBox();
        }

        if (!entered && stateInfo.normalizedTime >= 0.15f)
        {
            CameraShake cs = Camera.main.GetComponent<CameraShake>();
            cs.enabled = true;
            cs.StartCoroutine(cs.Shake(0.05f, 0.5f));
            entered = true;
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
        hitBox.StopCheckingCollision();

        //hitBox.StopCheckingCollision();
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
