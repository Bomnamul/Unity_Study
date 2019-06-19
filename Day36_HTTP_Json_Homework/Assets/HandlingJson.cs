using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class HandlingJson : MonoBehaviour
{
    string pathJson;

    // Start is called before the first frame update
    void Start()
    {
        //pathJson = Application.persistentDataPath + "/gameScore.json";
        pathJson = Application.persistentDataPath + "/jsonData.txt";
        print(pathJson);

        StartCoroutine(GetJsonFromURL());
    }

    IEnumerator GetJsonFromURL()
    {
        string url = "https://api.androidhive.info/contacts/";
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                print(uwr.error);
            }
            else
            {
                string dataAsJson = uwr.downloadHandler.text;
                File.WriteAllText(pathJson, dataAsJson);
                string loadJason = File.ReadAllText(pathJson);
                JsonData newJS = JsonUtility.FromJson<JsonData>(loadJason);
                print(newJS);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveJsonFile();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadJsonFile();
        }
    }

    private void LoadJsonFile()
    {
        if (File.Exists(pathJson))
        {
            string dataAsJson = File.ReadAllText(pathJson);
            GameScore newGS = JsonUtility.FromJson<GameScore>(dataAsJson);
            print(newGS);
        }
        else
        {
            print("No file !");
        }
    }

    private void SaveJsonFile()
    {
        GameScore gs = new GameScore();
        gs.level = 10;
        gs.timeElapsed = 300;
        gs.playerName = "dga";
        gs.dontCare = "Don't care";

        Item item = new Item();
        item.itemId = 1000;
        item.iconImage = "image1.png";
        item.price = 10000;
        gs.items = new List<Item>();
        gs.items.Add(item);
        Item item1 = new Item();
        item1.itemId = 2000;
        item1.iconImage = "image2.png";
        item1.price = 20000;
        gs.items.Add(item1);

        string dataAsJson = JsonUtility.ToJson(gs, true);   // JsonUtility는 배열을 인식하지 못함 (Wrapping)
        print(dataAsJson);
        File.WriteAllText(pathJson, dataAsJson);    // Sync(Block), Async version이 따로 있을 것
    }
}

[Serializable]
public class JsonData
{
    public List<Contact> contacts;

    public override string ToString()
    {
        return "" + contacts;
    }
}

[Serializable]
public class Contact
{
    public string id;
    public string name;
    public string email;
    public string address;
    public string gender;
    public object phone;
    //public string mobile;
    //public string home;
    //public string office;
}

[Serializable]    // Json 자체적으로는 어떻게 Serialize 할지 모르기에 명시해야함, Map은 Serialize 안됨 (list type으로 바꿔쓰던지 해야함)
public class GameScore
{
    public int level;
    public float timeElapsed;
    public string playerName;

    [NonSerialized]    // 제외
    public string dontCare;

    public List<Item> items;

    public override string ToString()
    {
        return level + ", " + timeElapsed + ", " + playerName + ", " + items.Count;
    }
}

[Serializable]
public class Item
{
    public int itemId;
    public string iconImage;
    public int price;
}