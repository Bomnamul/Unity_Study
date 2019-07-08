using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual IEnumerator FadeIn()
    {
        anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
    }

    public virtual IEnumerator FadeOut()
    {
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
    }
}
