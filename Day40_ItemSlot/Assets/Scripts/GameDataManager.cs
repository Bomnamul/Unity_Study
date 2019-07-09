using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Item
{
    public ItemData itemData;
}

[Serializable]
public class ItemDataJson
{
    public int itemTypeId;
    public string fileName;
}

[Serializable]
public class ItemListJson
{
    public List<ItemDataJson> itemDataList;
    public List<int> items;
}

public class GameDataManager : MonoBehaviour
{
    [SerializeField]
    Item[] items;

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
            if (items[i].itemData == null)
            {
                return i;
            }
        }
        return -1;
    }

    private void Start()
    {
        UpdateTimeStamp();
        StartCoroutine(GetItemsJsonFromURL());
    }

    public Item GetItem(int i)
    {
        if (i < items.Length && items[i].itemData != null)
        {
            return items[i];
        }
        return null;
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

    public Item[] GetItems()
    {
        return items;
    }

    public void AddItemAt(int i, ItemData itemData, bool redraw)
    {
        Item item = new Item();
        item.itemData = itemData;
        items[i] = item;
        if (redraw)
        {
            UpdateTimeStamp();
        }
    }

    public void RemoveItemAt(int i)
    {
        items[i].itemData = null;
        UpdateTimeStamp();
    }

    public void MoveItem(int from, int to, bool redraw)
    {
        if (from == to)
        {
            return;
        }

        SwapItem(from, to, false);
    }

    public void SwapItem(int i, int j, bool redraw)
    {
        Item a = items[i];
        items[i] = items[j];
        items[j] = a;
        if (redraw)
        {
            UpdateTimeStamp();
        }
    }

    IEnumerator GetItemsJsonFromURL()
    {
        string url = "http://localhost:3000/inven";
        using (var www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                print(www.error);
            }
            else
            {
                var json = www.downloadHandler.text;
                print(json);
                JsonToItems(json);
                UpdateTimeStamp();
            }
        }
    }

    private void JsonToItems(string json)
    {
        var list = JsonUtility.FromJson<ItemListJson>(json);
        var itemList = new List<Item>();
        var itemDataList = new List<ItemData>();
        foreach (var itemDataJson in list.itemDataList)
        {
            ItemData d = Resources.Load<ItemData>("Scriptable Objects/" + itemDataJson.fileName);
            if (d != null)
            {
                itemDataList.Add(d);
            }
            else
            {
                print("Failed itemData: " + itemDataJson.fileName);
            }
        }

        foreach (int itemTypeId in list.items)
        {
            int index = itemDataList.FindIndex(o => o.itemTypeId == itemTypeId);
            if (index != -1)
            {
                Item item = new Item();
                item.itemData = itemDataList[index];
                itemList.Add(item);
            }
            else
            {
                Item item = new Item();
                item.itemData = null;
                itemList.Add(item);
            }
        }

        this.items = itemList.ToArray();
    }

    public void UploadItems()
    {
        StartCoroutine(PostItemsJsonToURL());
    }

    IEnumerator PostItemsJsonToURL()
    {
        string json = ItemsToJson();

        string url = "http://localhost:3000/inven";
        using (var www = UnityWebRequest.Put(url, json))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            if (!www.isNetworkError && www.responseCode == 201)
            {
                print("Upload ok!");
            }
            else
            {
                print("Upload fail: " + www.responseCode);
            }
        }
    }

    public string ItemsToJson()
    {
        var itemList = new ItemListJson();
        var itemDataList = new List<ItemDataJson>();
        itemList.itemDataList = itemDataList;
        itemList.items = new List<int>();

        foreach (var item in this.items)
        {
            if (item.itemData != null)
            {
                if (!itemList.itemDataList.Exists(v => v.itemTypeId == item.itemData.itemTypeId))
                {
                    var itemDataJson = new ItemDataJson();
                    itemDataJson.itemTypeId = item.itemData.itemTypeId;
                    itemDataJson.fileName = item.itemData.name;
                    itemDataList.Add(itemDataJson);
                }
                itemList.items.Add(item.itemData.itemTypeId);
            }
            else
            {
                itemList.items.Add(0);
            }
        }

        string dataAsJson = JsonUtility.ToJson(itemList, true);
        return dataAsJson;
    }
}
