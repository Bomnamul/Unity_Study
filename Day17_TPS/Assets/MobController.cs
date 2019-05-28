using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public Transform player;
    public HitBox leftHandHitBox;
    public HitBox jumpAttackHitBox;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;
        float angle = Vector3.Angle(dir, transform.forward);
        anim.SetFloat("Angle", angle);
        anim.SetFloat("Distance", dir.magnitude);
    }
}
