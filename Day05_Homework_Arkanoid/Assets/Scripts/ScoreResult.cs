using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    Text scoreResult;
    // Start is called before the first frame update
    void Start()
    {
        scoreResult = GetComponent<Text>();
        scoreResult.text = "Highscore : " + ScoreManager.highScore.ToString();
    }
}
