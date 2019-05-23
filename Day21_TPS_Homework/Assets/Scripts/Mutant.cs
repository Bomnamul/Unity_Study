using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour
{
    public Transform target;

    Animator anim;
    Rigidbody rb;
    HurtBox hb;
    float dist;
    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponent<Rigidbody>();
        hb = GetComponentInChildren<HurtBox>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, target.position);
        if (dist >= 2)
        {
            transform.LookAt(target);
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
        }
        else
        {
            anim.SetTrigger("Attack");
        }

        if (transform.GetComponent<Health>().currentHealth <= 0)
        {
            anim.SetTrigger("Dying");
        }

        if (hb.state == ColliderState.Colliding)
        {
            anim.SetTrigger("Hurt");
        }
    }
}
