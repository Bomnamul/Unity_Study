using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    public static GameFlow instance;

    public GameObject playerPrefab;
    public RectTransform progressBar;

    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        SceneMgr.instance.LoadScene("Scene1");
    }

    //private void OnProgress(float progress)
    //{
    //    progressBar.GetComponent<Image>().fillAmount = progress;
    //}

    //private void OnBeginLoad()
    //{
    //    progressBar.parent.gameObject.SetActive(true);
    //}

    //private void OnLoadCompleted()
    //{
    //    //if (player == null)
    //    //{
    //    //    player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    //    //}
    //    //else
    //    //{
    //    //    //player.GetComponent<PlayerFSM>().onPortal = false;
    //    //}
    //    progressBar.parent.gameObject.SetActive(false);
    //}

    public void InstantiatePlayer()
    {
        if (player == null)
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
