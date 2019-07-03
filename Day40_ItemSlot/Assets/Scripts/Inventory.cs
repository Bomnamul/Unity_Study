using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] slots;

    // Start is called before the first frame update
    void Start()
    {
        List<Slot> list = new List<Slot>(FindObjectsOfType<Slot>()); // FindObjectsOfType: 순서가 보장안됨
        slots = new GameObject[list.Count];
        foreach (var s in list)
        {
            slots[s.slotId] = s.gameObject; // gameObject의 Id를 확인 후 list의 Id번째에 집어넣어 순서를 보장
        }
    }
}
