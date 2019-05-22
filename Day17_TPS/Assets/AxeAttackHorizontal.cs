using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttackHorizontal : StateMachineBehaviour, IHitBoxResponder
{
    //Transform axe;

    public int damage = 5;

    HitBox hitBox;
    Dictionary<int, int> hitObjects;

    //HitBox hitBox;
    //bool isDamaged = false;

    public void CollisionWith(Collider collider)
    {
        HurtBox hurtBox = collider.GetComponent<HurtBox>();
        int id = collider.transform.root.gameObject.GetInstanceID();
        if (!hitObjects.ContainsKey(id)) // GetInstanceID: Object의 unique한 id (hash key로 사용 가능)
        {
            hitObjects[id] = 1;
        }
        else
        {
            hitObjects[id] += 1;
            return;
        }

        hurtBox.GetHitby(damage); // debugging
        //collider.GetComponentInParent<Health>().DecreaseHP(damage);
        Vector3 cameraTargetPosition = hitBox.transform.root.Find("CameraTarget").transform.position;
        RaycastHit hit;
        Vector3 hitPoint = collider.transform.position;
        Vector3 hitNormal = cameraTargetPosition - hitPoint;
        hitNormal = hitNormal.normalized;
        Vector3 hitDirection = -hitNormal;
        if (Physics.Raycast(cameraTargetPosition, 
                            hitDirection, 
                            out hit, 
                            2f, 
                            1 << LayerMask.NameToLayer("HurtBox"), // int layerMask
                            QueryTriggerInteraction.Collide)) // <<: HurtBox만 사용하겠다, bit 연산
        {
            hitPoint = hit.point;
            hitNormal = hit.normal;
            hitDirection = hitPoint - cameraTargetPosition;
            hitDirection = hitDirection.normalized;
        }

        Debug.Log("1 Hit: " + collider.name);
        Debug.DrawLine(cameraTargetPosition, hitPoint, Color.yellow, 2f);
        Debug.DrawLine(hitPoint, hitPoint + hitNormal, Color.magenta, 2f);
        Debug.DrawLine(hitPoint, hitPoint + hitDirection, Color.cyan, 2f);

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
        hitBox = animator.GetComponent<PlayerController>().weaponHolder.GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
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
        if (0.35f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.45f)
        {
            hitBox.UpdateHitBox();
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
