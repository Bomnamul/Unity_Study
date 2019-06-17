using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleChat : MonoBehaviour
{
    public bool leftSide = true;

    HorizontalLayoutGroup horizontalLayout;
    Transform bubble;
    Image bubbleColor;
    Color gray = new Color(0.5f, 0.5f, 0.5f, 1f);
    Color blue = new Color(0.25f, 0.5f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        horizontalLayout = GetComponent<HorizontalLayoutGroup>();
        bubble = transform.GetChild(0);
        bubbleColor = bubble.GetComponent<Image>();

        if (leftSide)
        {
            horizontalLayout.padding.left = 0;
            horizontalLayout.padding.right = 200;
            horizontalLayout.childAlignment = TextAnchor.MiddleLeft;
            bubble.rotation = Quaternion.identity;
            bubble.GetChild(0).rotation = Quaternion.identity;
            bubbleColor.color = gray;
        }
        else
        {
            horizontalLayout.padding.left = 200;
            horizontalLayout.padding.right = 0;
            horizontalLayout.childAlignment = TextAnchor.MiddleRight;
            bubble.rotation = Quaternion.Euler(0f, 180f, 0f);
            bubble.GetChild(0).rotation = Quaternion.identity;
            bubbleColor.color = blue;
        }
    }
}
