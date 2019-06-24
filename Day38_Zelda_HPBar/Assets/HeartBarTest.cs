using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBarTest : MonoBehaviour
{
    public Button takeDamageButton;
    public Button healButton;
    public HeartBar bar;

    private void Awake()
    {
        takeDamageButton.onClick.AddListener(() =>
        {
            bar.DecreaseHP(15);
        });

        healButton.onClick.AddListener(() =>
        {
            bar.IncreaseHP(7);
        });
    }
}
