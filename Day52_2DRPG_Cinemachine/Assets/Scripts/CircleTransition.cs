using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTransition : SceneTransition
{
    public override IEnumerator FadeIn()
    {
        if (GameFlow.instance.player != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(GameFlow.instance.player.transform.position);
        }
        return base.FadeIn();
    }

    public override IEnumerator FadeOut()
    {
        if (GameFlow.instance.player != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(GameFlow.instance.player.transform.position);
        }
        return base.FadeOut();
    }
}
