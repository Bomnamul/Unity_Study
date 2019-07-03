using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Pickup : MonoBehaviour
{
    public GameObject itemButton;
    public ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var s in UIController.instance.bag.GetComponent<Inventory>().slots)
            {
                if (s.transform.childCount == 0)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                    var button = Instantiate(itemButton, s.transform, false);
                    button.transform.localScale *= 0.8f;
                    button.transform.position = screenPos;
                    button.transform.DOMove(s.transform.position, 0.4f).SetEase(Ease.OutExpo).OnComplete(() =>
                    {
                        BounceAnim(button.transform);
                        GameDataManager.instance.AddItemAt(s.GetComponent<Slot>().slotId, itemData);
                    });
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void BounceAnim(Transform t)
    {
        t.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            t.localScale = Vector3.one;
        });
    }
}
