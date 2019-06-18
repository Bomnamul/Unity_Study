using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    public AppSystem appSystem;

    int uiTimeStamp = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int timeStamp = AppDataManager.Instance.GetTimeStamp();
        if (timeStamp != uiTimeStamp)
        {
            uiTimeStamp = timeStamp;
            List<PostData> posts = AppDataManager.Instance.GetPostData();
            appSystem.UpdatePostData(posts);
        }
    }
}
