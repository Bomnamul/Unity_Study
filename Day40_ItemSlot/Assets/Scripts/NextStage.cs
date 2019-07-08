using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextStage : MonoBehaviour
{
    public string nextStage;
    public SceneTransition sceneTransition;
    public AudioSource music;

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject player = GameFlow.instance.player;
            player.GetComponent<PlayerFSM>().movable = false;
            player.transform.position = transform.position + Vector3.up * -1.5f;
            transform.GetChild(0).DOLocalMoveX(-1, 0.5f);  // Coroutine 처럼 작동
            transform.GetChild(1).DOLocalMoveX(1, 0.5f);
            yield return new WaitForSeconds(0.5f);
            player.transform.DOMoveY(1.5f, 0.5f).SetRelative();
            player.transform.DOScale(0.5f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            //player.GetComponent<Renderer>().enabled = false;
            player.transform.localScale = Vector3.one;
            music?.DOFade(0f, 1f);
            yield return StartCoroutine(sceneTransition.FadeIn());
            SceneMgr.instance.LoadScene(nextStage);
            UIController.instance.bag.Hide();
            //transform.GetComponent<Animator>().SetTrigger("OpenDoor");
            //collision.GetComponent<PlayerFSM>().onPortal = true;
        }
    }
}
