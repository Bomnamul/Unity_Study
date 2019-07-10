using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Draggable : MonoBehaviour,
                         IBeginDragHandler, IDragHandler, IEndDragHandler,
                         IPointerEnterHandler, IPointerExitHandler
{
    static Transform draggingItemButton; // Slot마다 Draggable을 가지고 있고 공통적으로 사용할 부분이 필요할 경우 static 사용
    static Slot enteredSlot;
    static Slot beginSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            beginSlot = GetComponent<Slot>();
            Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
            if (item.itemData == null)
            {
                return;
            }

            draggingItemButton = transform.GetChild(0);
            draggingItemButton.GetComponent<Image>().raycastTarget = false;
            Transform canvas = GetComponentInParent<Canvas>().transform;
            draggingItemButton.SetParent(canvas, false);
            draggingItemButton.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingItemButton != null)
        {
            draggingItemButton.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (enteredSlot != null && draggingItemButton != null) // Swap or Move
        {
            if (enteredSlot.transform.childCount > 0) // Swap
            {
                //print("Swap");
                Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
                if (item.itemData == null)
                {
                    return;
                }

                Item item2 = GameDataManager.instance.GetItem(enteredSlot.slotId);
                if (item.itemData == null)
                {
                    return;
                }

                Transform otherButton = enteredSlot.transform.GetChild(0);
                draggingItemButton.SetParent(enteredSlot.transform, false);
                draggingItemButton.localPosition = Vector3.zero;
                otherButton.SetParent(beginSlot.transform, false);
                BounceAnim(draggingItemButton.transform);
                int beginId = beginSlot.slotId;
                int destId = enteredSlot.slotId;
                GameDataManager.instance.SwapItem(beginId, destId, false);
            }
            else // Move
            {
                //print("Move");
                Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
                if (item.itemData == null)
                {
                    return;
                }

                draggingItemButton.SetParent(enteredSlot.transform, false);
                draggingItemButton.localPosition = Vector3.zero;
                BounceAnim(draggingItemButton.transform);
                int fromId = beginSlot.slotId;
                int toId = enteredSlot.slotId;
                GameDataManager.instance.MoveItem(from: fromId, to: toId, redraw: false);
            }
            draggingItemButton.GetComponent<Image>().raycastTarget = true;
        }
        else if (enteredSlot == null && draggingItemButton !=null) // Item Drop
        {
            // Rollback
            //draggingItemButton.SetParent(transform, false);
            //draggingItemButton.localPosition = Vector3.zero;

            //print("drop");
            Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
            if (item.itemData == null)
            {
                return;
            }
            draggingItemButton.GetComponent<Spawn>().SpawnItem();
            Destroy(draggingItemButton.gameObject);
            GameDataManager.instance.RemoveItemAt(beginSlot.slotId);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        enteredSlot = GetComponent<Slot>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        enteredSlot = null;
    }

    private void BounceAnim(Transform t)
    {
        t.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            t.localScale = Vector3.one;
        });
    }
}
