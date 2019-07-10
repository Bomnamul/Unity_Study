using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    public int itemTypeId;
    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject itemButtonPrefab;
}
