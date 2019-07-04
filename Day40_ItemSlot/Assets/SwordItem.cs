using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : MonoBehaviour
{
    public GameObject itemPrefab;

    public void Use()
    {
        GameObject player = GameFlow.instance.player;
        Instantiate(itemPrefab, player.transform.position + Vector3.one * 0.5f, Quaternion.identity, player.transform);
        GameDataManager.instance.RemoveItemAt(GetComponentInParent<Slot>().slotId);
        Destroy(gameObject);
    }
}
