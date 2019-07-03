using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public void Use()
    {
        GameDataManager.instance.RemoveItemAt(GetComponentInParent<Slot>().slotId);
        Destroy(gameObject);
    }
}
