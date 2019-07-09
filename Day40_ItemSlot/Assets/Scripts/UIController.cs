using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public ProgressBar progressBar;
    public Bag bag;

    int uiTimeStamp = 0;
    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        SceneMgr.instance.OnBeginLoad += progressBar.OnBeginLoad;
        SceneMgr.instance.OnLoadCompleted += progressBar.OnLoadCompleted;
        SceneMgr.instance.OnProgress += progressBar.OnProgress;
    }

    private void Update()
    {
        int timeStamp = GameDataManager.instance.GetTimeStamp();
        if (timeStamp != uiTimeStamp)
        {
            uiTimeStamp = timeStamp;
            Item[] items = GameDataManager.instance.GetItems();
            bag.UpdateBag(items);
        }
    }
}
