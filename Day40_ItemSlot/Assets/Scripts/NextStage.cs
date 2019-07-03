using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextStage : MonoBehaviour
{
    public string nextStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.GetChild(0).DOLocalMoveX(-1, 1);  // Coroutine 처럼 작동
            transform.GetChild(1).DOLocalMoveX(1, 1);
            SceneMgr.instance.LoadScene(nextStage);
            UIController.instance.bag.Hide();
            //transform.GetComponent<Animator>().SetTrigger("OpenDoor");
            //collision.GetComponent<PlayerFSM>().onPortal = true;
        }
    }
}
