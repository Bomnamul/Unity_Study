using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Health leftSide;
    public Health rightSide;
    public Button healButton;
    public Button damageButton;
    public Text timeText;

    int uiTimeStamp = 0;
    int currentTime;

    // Start is called before the first frame update
    void Start()
    {
        damageButton.onClick.AddListener(() => GameDataManager.Instance.TakeDamage(10f));
        healButton.onClick.AddListener(() => GameDataManager.Instance.Heal(20f));
        currentTime = GameDataManager.Instance.GetTimeCount();
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            GameDataManager.Instance.SetTimeCount(currentTime--);
        }
    }

    // Update is called once per frame
    void Update() // MVC (Model View Controller)
    {
        int timeStamp = GameDataManager.Instance.GetTimeStamp();
        if (timeStamp != uiTimeStamp) // Polling
        {
            uiTimeStamp = timeStamp;
            float currentHealth = GameDataManager.Instance.GetCurrentHealth();
            float maxHealth = GameDataManager.Instance.GetMaxHealth();
            timeText.text = currentTime.ToString();
            leftSide.UpdateHealthBar(currentHealth, maxHealth);
            rightSide.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
