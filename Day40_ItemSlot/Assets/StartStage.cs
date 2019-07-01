using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStage : MonoBehaviour
{
    // Start is called before the first frame update
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
            player.transform.position = entry.transform.position + Vector3.right * 2f;
        }
        else // Title -> Scene1
        {
            player.transform.position = transform.position;
        }
    }
}
