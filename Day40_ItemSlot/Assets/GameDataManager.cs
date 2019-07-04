using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField]
    ItemData[] items;

    int timeStamp = 0;

    public static GameDataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public int FindEmptySlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    private void Start()
    {
        UpdateTimeStamp();
    }

    public ItemData GetItem(int i)
    {
        return items[i];
    }

    void UpdateTimeStamp()
    {
        timeStamp++;
        if (timeStamp <= 0)
        {
            timeStamp = 1;
        }
    }

    public int GetTimeStamp()
    {
        return timeStamp;
    }

    public ItemData[] GetItems()
    {
        return items;
    }

    public void AddItemAt(int i, ItemData itemData, bool redraw)
    {
        items[i] = itemData;
        if (redraw)
        {
            UpdateTimeStamp();
        }
    }

    public void RemoveItemAt(int i)
    {
        items[i] = null;
        UpdateTimeStamp();
    }

    public void MoveItem(int from, int to, bool redraw)
    {
        if (from == to)
        {
            return;
        }

        items[to] = items[from];
        items[from] = null;
        if (redraw)
        {
            UpdateTimeStamp();
        }
    }

    public void SwapItem(int i, int j, bool redraw)
    {
        ItemData a = items[i];
        items[i] = items[j];
        items[j] = a;
        if (redraw)
        {
            UpdateTimeStamp();
        }
    }
}
