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
            int emptySlotId = GameDataManager.instance.FindEmptySlot();
            if (emptySlotId != -1)
            {
                GameDataManager.instance.AddItemAt(emptySlotId, itemData, false);

                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                Transform emptySlot = UIController.instance.bag.GetComponent<Inventory>().slots[emptySlotId].transform;
                var button = Instantiate(itemButton, emptySlot, false);
                button.transform.localScale *= 0.8f;
                button.transform.position = screenPos;
                button.transform.DOMove(emptySlot.position, 0.4f).SetEase(Ease.OutExpo).OnComplete(() =>
                {
                    BounceAnim(button.transform);
                });
                Destroy(gameObject);
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
