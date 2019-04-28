using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
            ScoreManager.highScore = 0;
            ScoreManager.life = 2;
        }
    }
}
