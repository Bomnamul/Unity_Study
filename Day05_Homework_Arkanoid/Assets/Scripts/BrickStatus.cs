using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickStatus : MonoBehaviour
{
    private int bricks;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
}
