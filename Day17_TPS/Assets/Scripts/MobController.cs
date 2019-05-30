using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public Transform player;
    public HitBox leftHandHitBox;
    public HitBox jumpAttackHitBox;
    public GameObject jumpAttackFX;
    public Vector3 moveDirection;

    Animator anim;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        float angle = Vector3.Angle(dir, transform.forward);
        anim.SetFloat("Angle", angle);
        anim.SetFloat("HorizontalDistance", dir.magnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection);
        moveDirection = Vector3.zero;
    }
}
