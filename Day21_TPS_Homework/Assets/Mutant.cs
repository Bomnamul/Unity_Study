using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour
{
    Animator anim;
    HurtBox hb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        hb = GetComponent<HurtBox>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hb.state == ColliderState.Colliding && !anim.IsInTransition(0))
        {
            anim.SetTrigger("Hurt");
        }
    }
}
