using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadTexture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTextureFromURL());
    }

    IEnumerator GetTextureFromURL()
    {
        string url = "https://avatars1.githubusercontent.com/u/43165018?s=460&v=4";

        // 1.
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))    // 알아서 반납
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                print(uwr.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(uwr);
                GetComponent<Renderer>().material.mainTexture = texture;
            }
        }

        // 2.
        //{
        //    UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        //    try
        //    {
        //        yield return uwr.SendWebRequest();
        //        if (uwr.isNetworkError || uwr.isHttpError)
        //        {
        //            print(uwr.error);
        //        }
        //        else
        //        {
        //            var texture = DownloadHandlerTexture.GetContent(uwr);
        //            GetComponent<Renderer>().material.mainTexture = texture;
        //        }
        //    }
        //    finally
        //    {
        //        if (uwr != null)
        //            uwr.Dispose();  // 메모리에서 제거 요청 (OS측면에서 중요한 자원이기에 Dispose로 항상 제거해야함)
        //    }
        //}

        // 3.
        //using (WWW www = new WWW(url))
        //{
        //    yield return www;
        //    GetComponent<Renderer>().material.mainTexture = www.texture;
        //}
    }
}
