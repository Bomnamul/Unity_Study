using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextStage : MonoBehaviour
{
    public string nextStage;
    public bool sceneLoadEnabled = true;
    public SceneTransition sceneTransition;
    public AudioSource music;

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && sceneLoadEnabled)
        {
            sceneLoadEnabled = false;
            GameObject player = GameFlow.instance.player;
            player.GetComponent<PlayerFSM>().controllable = false;
            player.transform.position = transform.position + Vector3.up * -1.5f;
            player.GetComponent<PlayerFSM>().lookAtHere = Vector3.up;
            yield return null;

            transform.GetChild(0).DOLocalMoveX(-1, 0.5f);  // Coroutine 처럼 작동
            transform.GetChild(1).DOLocalMoveX(1, 0.5f);
            yield return new WaitForSeconds(0.5f);

            player.transform.DOMoveY(1.8f, 0.5f).SetRelative();
            player.transform.DOScale(0.5f, 0.5f);
            yield return new WaitForSeconds(0.5f);

            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<PlayerFSM>().lookAtHere = Vector3.down;
            //player.transform.localScale = Vector3.one;
            music?.DOFade(0f, 1f);
            yield return StartCoroutine(sceneTransition.FadeIn());
            player.GetComponent<Renderer>().enabled = true;
            SceneMgr.instance.LoadScene(nextStage);
            UIController.instance.bag.Hide();
            sceneLoadEnabled = true;
            //transform.GetComponent<Animator>().SetTrigger("OpenDoor");
            //collision.GetComponent<PlayerFSM>().onPortal = true;
        }
    }
}
