using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Health Heart;
    public Button healButton;
    public Button damageButton;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageButton.onClick.AddListener(() => GameDataManager.Instance.TakeDamage(25f));
        healButton.onClick.AddListener(() => GameDataManager.Instance.Heal(50f));
    }

    void Update() // MVC (Model View Controller)
    {
        int timeStamp = GameDataManager.Instance.GetTimeStamp();
        if (timeStamp != uiTimeStamp) // Polling
        {
            uiTimeStamp = timeStamp;
            float currentHealth = GameDataManager.Instance.GetCurrentHealth();
            float maxHealth = GameDataManager.Instance.GetMaxHealth();
            Heart.UpdateHealthBar(currentHealth, maxHealth);
            Heart.UpdateHealthBarGlow(currentHealth, maxHealth);
        }
    }
}
