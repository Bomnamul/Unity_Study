using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetJsonFromURL());
    }

    IEnumerator GetJsonFromURL()
    {
        // Dummy json: https://jsonplaceholder.typicode.com/
        string url = "https://jsonplaceholder.typicode.com/todos/1";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)  // Network가 끊겼는지, Protocol 규약에 어긋나는지 Check
            {
                print(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                print(json);
            }
        }
    }
}
