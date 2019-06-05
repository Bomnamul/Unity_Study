﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : StateMachineBehaviour, IHitBoxResponder
{
    //Transform axe;

    public int damage = 5;
    public bool enabledMultipleHits = false;
    //public GameObject hb; // MonoBehaviour가 아니라 못씀

    HitBox hitBox;
    bool entered;

    //HitBox hitBox;
    //bool isDamaged = false;

    public void CollisionWith(Collider collider, HitBox hitBox)
    {
        HurtBox hurtBox = collider.GetComponent<HurtBox>();

        hurtBox.GetHitby(damage); // debugging
        //collider.GetComponentInParent<Health>().DecreaseHP(damage);

        Vector3 from = hitBox.transform.position;
        Vector3 hitPoint;
        Vector3 hitNormal;
        Vector3 hitDirection;

        hitBox.GetContactInfo(from: from,
                              to: collider.ClosestPoint(from),
                              out hitPoint, out hitNormal, out hitDirection,
                              2f);

        BoxHitReaction hr = collider.GetComponentInParent<BoxHitReaction>();
        hr?.Hurt(damage, hitPoint, hitNormal, hitDirection, ReactionType.Stun);

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
        hitBox = animator.GetComponent<MobController>().jumpAttackHitBox;
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
        if (0.56f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.80f)
        {
            hitBox.UpdateHitBox();
        }
        if (!entered && stateInfo.normalizedTime >= 0.6f)
        {
            MobController mc = animator.GetComponent<MobController>();
            var fx = Instantiate(mc.jumpAttackFX, mc.transform.position, Quaternion.identity);
            Destroy(fx, 2f);

            AddForceToEnv(200f, hitBox.transform.position, 5f, 1f);

            CameraShake cs = Camera.main.GetComponent<CameraShake>();
            cs.enabled = true;
            cs.StartCoroutine(cs.Shake(0.1f, 0.4f));
            entered = !entered;
        }
    }

    private void AddForceToEnv(float power, Vector3 explosionPosition, float radius, float upwardsModifier) // upwardsModifier: 윗쪽 방향으로 힘을 줄지말지
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius); // explosionPosition 위치의 radius 반경반큼의 collider를 다 구해줌
        foreach (var c in colliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upwardsModifier);
            }
        }
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