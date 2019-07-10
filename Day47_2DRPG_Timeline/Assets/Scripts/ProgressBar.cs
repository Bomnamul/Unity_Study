using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    Image foreground;

    private void Awake()
    {
        foreground = transform.Find("Foreground").GetComponent<Image>();
    }

    public void OnProgress(float progress)
    {
        foreground.fillAmount = progress;
    }

    public void OnBeginLoad()
    {
        gameObject.SetActive(true);
    }

    public void OnLoadCompleted()
    {
        gameObject.SetActive(false);
    }

}
