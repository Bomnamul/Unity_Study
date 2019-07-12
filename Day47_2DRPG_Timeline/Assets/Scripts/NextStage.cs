using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NextStage : MonoBehaviour
{
    public string nextStage;
    public bool sceneLoadEnabled = true;
    public SceneTransition sceneTransition;
    public AudioSource music;

    PlayableDirector pd;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && sceneLoadEnabled)
        {
            sceneLoadEnabled = false;
            GameObject player = GameFlow.instance.player;
            player.GetComponent<PlayerFSM>().controllable = false;
            player.transform.position = transform.position + Vector3.up * -1.5f;

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
            yield return new WaitForSeconds(1f);

            player.transform.Find("Model").GetComponent<Renderer>().enabled = false;
            //player.GetComponent<PlayerFSM>().lookAtHere = Vector3.down;
            player.transform.localScale = Vector3.one;
            music?.DOFade(0f, 1f);
            yield return StartCoroutine(sceneTransition.FadeIn());
            player.transform.Find("Model").GetComponent<Renderer>().enabled = true;
            SceneMgr.instance.LoadScene(nextStage);
            UIController.instance.bag.Hide();
            sceneLoadEnabled = true;
        }
    }
}
