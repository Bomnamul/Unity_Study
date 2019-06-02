using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReactionType
{
    None = 0,
    Head,
    Body,
    Stomach,
    Bottom,
    Stun
}

public class BoxHitReaction : MonoBehaviour
{
    public GameObject hitFXPrefab;
    public GameObject stunFXPrefab;
    public Transform stunFXPos;

    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void Hurt(int damage, Vector3 hitPoint, Vector3 hitNormal, Vector3 hitDirection, ReactionType reactionType)
    {
        if (anim != null 
            && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Reaction")
            && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Invincible")
            && !anim.IsInTransition(0))
        {
            anim.SetTrigger("Reaction");
            anim.SetInteger("ReactionType", (int)reactionType); // enum type이라 casting 필요

            if (reactionType == ReactionType.Stun)
            {
                GameObject stunfx = Instantiate(stunFXPrefab, stunFXPos.position, Quaternion.LookRotation(Vector3.up), stunFXPos);
                Destroy(stunfx, 3.2f);
            }
        }

        GetComponent<Health>().DecreaseHP(damage);
        GameObject fx = Instantiate(hitFXPrefab, hitPoint, Quaternion.identity);
        Destroy(fx, 1.5f);

        //rb?.AddForce(hitDirection, ForceMode.VelocityChange); // 둘의 사용법이 미묘하게 다르다

        // 결과가 다르다
        if (rb != null)
        {
            rb.velocity = hitDirection;
        }

        //if (rb != null)
        //{
        //    rb.velocity += hitDirection;
        //}
    }
}
