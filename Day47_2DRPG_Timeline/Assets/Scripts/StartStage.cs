using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;

public class StartStage : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public AudioSource music;

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
    }

    IEnumerator Start()
    {
        List<NextStage> entries = new List<NextStage>(FindObjectsOfType<NextStage>());  // Component List
        var entry = entries.Find(o => o.nextStage == SceneMgr.instance.prevScene);
        if (entry != null)
        {
            pd = entry.GetComponent<PlayableDirector>();

            entry.sceneLoadEnabled = false;
            player.transform.position = entry.transform.position;
            StartCoroutine(sceneTransition.FadeOut());
            music?.DOFade(1f, 1f);
            player.GetComponent<Renderer>().enabled = false;
            //entry.transform.GetChild(0).DOLocalMoveX(-1, 0.5f);
            //entry.transform.GetChild(1).DOLocalMoveX(1, 0.5f);
            pd.Play();
            yield return new WaitForSeconds(0.5f);
            player.transform.localScale *= 0.5f;
            player.GetComponent<Renderer>().enabled = true; // Lambda Capturing: null reference의 가능성이 크니 주의해서 사용(reference의 생존을 보장해줘야 함)
            player.GetComponent<PlayerFSM>().lookAtHere = Vector3.down;
            player.transform.DOMoveY(-1.8f, 0.5f).SetRelative();
            player.transform.DOScale(1f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            entry.sceneLoadEnabled = true;
            UIController.instance.bag.Show();
            yield return new WaitForSeconds(0.5f);
            player.GetComponent<PlayerFSM>().controllable = true;
            //entry.transform.GetChild(0).DOLocalMoveX(-0.48f, 0.5f);
            //entry.transform.GetChild(1).DOLocalMoveX(0.48f, 0.5f);
        }
        else // Title -> Scene1
        {
            player.transform.position = transform.position;
            UIController.instance.bag.Show();
            StartCoroutine(sceneTransition.FadeOut());
        }
    }
}
