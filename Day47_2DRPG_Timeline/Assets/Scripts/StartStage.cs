using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System;

public class StartStage : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public AudioSource music;
    public PlayableAsset exitTimelineAsset;

    GameObject player;
    PlayableDirector pd;

    private void Awake()
    {
        player = GameFlow.instance.player;
        if (player == null)
        {
            GameFlow.instance.InstantiatePlayer();
            player = GameFlow.instance.player;
        }
        DeactivateEditorOnlyObjects();
    }

    private void DeactivateEditorOnlyObjects()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("EditorOnly"))
        {
            o.SetActive(false);
        }
    }

    IEnumerator Start()
    {
        List<NextStage> entries = new List<NextStage>(FindObjectsOfType<NextStage>());  // Component List
        var entry = entries.Find(o => o.nextStage == SceneMgr.instance.prevScene);
        if (entry != null)
        {
            pd = entry.GetComponent<PlayableDirector>();
            PlayableAsset back = pd.playableAsset;
            pd.playableAsset = exitTimelineAsset;

            entry.sceneLoadEnabled = false;
            player.transform.position = entry.transform.position;
            StartCoroutine(sceneTransition.FadeOut());
            music?.DOFade(1f, 1f);
            player.transform.Find("Model").GetComponent<Renderer>().enabled = false;

            var timelineAsset = pd.playableAsset as TimelineAsset; // Binding, as (casting): 캐스팅되면 넣고 실패하면 null
            if (timelineAsset == null)
            {
                yield break;
            }

            foreach (var track in timelineAsset.GetOutputTracks())
            {
                var animTrack = track as AnimationTrack;
                if (animTrack == null)
                {
                    continue;
                }

                print(animTrack.name);
                if (animTrack.name == "Player")
                {
                    animTrack.position = player.transform.position;
                }
            }

            foreach (var track in timelineAsset.outputs)
            {
                if (track.streamName == "Player")
                {
                    pd.SetGenericBinding(track.sourceObject, player);
                }

                if (track.streamName == "Player Animation")
                {
                    pd.SetGenericBinding(track.sourceObject, player.transform.Find("Model").GetComponent<Animator>());
                }
            }

            pd.Play();
            yield return new WaitForSeconds(0.5f);
            player.transform.Find("Model").GetComponent<Renderer>().enabled = true; // Lambda Capturing: null reference의 가능성이 크니 주의해서 사용(reference의 생존을 보장해줘야 함)
            //player.GetComponent<PlayerFSM>().lookAtHere = Vector3.down;
            yield return new WaitForSeconds(0.5f);
            entry.sceneLoadEnabled = true;
            UIController.instance.bag.Show();
            yield return new WaitForSeconds(0.5f);
            pd.playableAsset = back;
            player.GetComponent<PlayerFSM>().controllable = true;
        }
        else // Title -> Scene1
        {
            player.transform.position = transform.position;
            UIController.instance.bag.Show();
            StartCoroutine(sceneTransition.FadeOut());
        }
    }
}
