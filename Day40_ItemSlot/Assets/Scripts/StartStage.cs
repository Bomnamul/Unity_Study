using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartStage : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public AudioSource music;
    
    void Start()
    {
        var player = GameFlow.instance.player;
        if (player == null)
        {
            GameFlow.instance.InstantiatePlayer();
            player = GameFlow.instance.player;
        }

        List<NextStage> entries = new List<NextStage>(FindObjectsOfType<NextStage>());  // Component List
        var entry = entries.Find(o => o.nextStage == SceneMgr.instance.prevScene);
        if (entry != null)
        {
            player.transform.position = entry.transform.position + Vector3.up * -2f;
        }
        else // Title -> Scene1
        {
            player.transform.position = transform.position;
        }

        StartCoroutine(sceneTransition.FadeOut());
        music?.DOFade(1f, 1f);
        player.GetComponent<PlayerFSM>().movable = true;
        //player.GetComponent<Renderer>().enabled = true;

        UIController.instance.bag.Show();
    }
}
