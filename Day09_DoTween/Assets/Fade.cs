using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>
            //{
            //    // root.gameObject.SetActive(false);
            //});

            DOTween.Sequence() // Chaining
                .Append(GetComponent<Image>().DOFade(0f, 2f))
                .Append(GetComponent<Image>().DOFade(1f, 2f))
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    print("callback");
                });
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            DOTween.KillAll();
        }
    }
}
