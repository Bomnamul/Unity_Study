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
    public float damage;
    public float heal;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageButton.onClick.AddListener(() => GameDataManager.Instance.TakeDamage(damage));
        healButton.onClick.AddListener(() => GameDataManager.Instance.Heal(heal));
    }

    void Update() // MVC (Model View Controller)
    {
        int timeStamp = GameDataManager.Instance.GetTimeStamp();
        if (timeStamp != uiTimeStamp) // Polling
        {
            uiTimeStamp = timeStamp;
            float prevHealth = GameDataManager.Instance.GetPrevHealth();
            float currentHealth = GameDataManager.Instance.GetCurrentHealth();
            float maxHealth = GameDataManager.Instance.GetMaxHealth();
            Heart.UpdateHealthBar(prevHealth, currentHealth, maxHealth);
        }
    }
}
