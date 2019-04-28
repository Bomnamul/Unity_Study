using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (ScoreManager.life > 0)
        {
            ScoreManager.life--;
            SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
            ScoreManager.score = 0;
        }
        else
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            ScoreManager.score = 0;
        }
    }
}
