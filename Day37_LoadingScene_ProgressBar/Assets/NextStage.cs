using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public string nextStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneMgr.instance.LoadScene(nextStage);
            transform.GetComponent<Animator>().SetTrigger("OpenDoor");
            collision.GetComponent<PlayerFSM>().onPortal = true;
        }
    }
}
