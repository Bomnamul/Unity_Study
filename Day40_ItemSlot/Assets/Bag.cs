using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bag : MonoBehaviour
{
    bool isActive = false;

    public void Show()
    {
        GetComponent<RectTransform>().DOMoveY(0, 0.5f);
        isActive = true;
    }

    public void Hide()
    {
        GetComponent<RectTransform>().DOMoveY(-125, 0.5f);
        isActive = false;
    }

    public void OnOff()
    {
        if (isActive)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOff();
        }
    }
}
