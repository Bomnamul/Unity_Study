using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHitReaction : MonoBehaviour
{
    public GameObject hitFXPrefab;

    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void Hurt(int damage, Vector3 hitPoint, Vector3 hitNormal, Vector3 hitDirection)
    {
        if (anim != null)
        {
            anim.SetTrigger("Reaction");
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
