using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    Animator anim;
    Image img;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
    }

    public virtual IEnumerator FadeIn()
    {
        img.enabled = true;
        anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
    }

    public virtual IEnumerator FadeOut()
    {
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        img.enabled = false;
    }
}
