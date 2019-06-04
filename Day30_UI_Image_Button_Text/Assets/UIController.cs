using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Health leftSide;
    public Button healButton;
    public Button damageButton;
    public Text timeText;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageButton.onClick.AddListener(() => GameDataManager.Instance.TakeDamage(10f));
        healButton.onClick.AddListener(() => GameDataManager.Instance.Heal(20f));
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
            leftSide.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
