using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScriptableObject : MonoBehaviour
{
    [SerializeField]
    ItemData healthPotion;

    void Start()
    {
        healthPotion = Resources.Load<ItemData>("Scriptable Objects/HealthPotion"); // 앞에 /가 없으면 현재 디렉토리 기준(상대 경로), 있을 경우 Root 기준
    }
}
