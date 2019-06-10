using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button button;
    public static bool isGameOver = false;

    private void Start()
    {
        button.onClick.AddListener(() => isGameOver = !isGameOver);
    }
}
