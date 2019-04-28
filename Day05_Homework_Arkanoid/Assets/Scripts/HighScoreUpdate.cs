using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUpdate : MonoBehaviour
{
    Text highScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        highScoreLabel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.score >= ScoreManager.highScore)
        {
            ScoreManager.highScore = ScoreManager.score;
        }
        highScoreLabel.text = ScoreManager.highScore.ToString();
    }
}
