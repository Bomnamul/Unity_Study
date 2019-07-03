using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Bag : MonoBehaviour
{
    bool isActive = false;

    public void Show()
    {
        GetComponent<RectTransform>().DOMoveY(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            isActive = true;
        });
    }

    public void Hide()
    {
        GetComponent<RectTransform>().DOMoveY(-125, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            isActive = false;
        });
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

    public void UpdateBag(ItemData[] items)
    {
        GameObject[] slots = GetComponent<Inventory>().slots;
        int i = 0;
        foreach (var item in items)
        {
            if (item !=null)
            {
                var s = slots[i];
                foreach(Transform child in s.transform)
                {
                    Destroy(child.gameObject);
                }
                Instantiate(item.itemButtonPrefab, s.transform, false);
            }
            i++;
        }
    }
}
