using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public GameObject healthEffect;

    public void Use()
    {
        var fx = Instantiate(healthEffect, GameFlow.instance.player.transform.position, Quaternion.identity);
        Destroy(fx, 2f);
        GameDataManager.instance.RemoveItemAt(GetComponentInParent<Slot>().slotId);
        Destroy(gameObject);
    }
}
