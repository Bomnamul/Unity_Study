using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    protected GameDataManager() { }

    float currentHealth = 100f;
    float maxHealth = 100f;
    int timeCount = 99;

    int timeStamp = 0;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }
        UpdateTimeStamp();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateTimeStamp();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetTimeCount()
    {
        return timeCount;
    }
    
    public void SetTimeCount(int t)
    {
        timeCount = t;
        UpdateTimeStamp();
    }

    void UpdateTimeStamp() // Dirty bit?
    {
        timeStamp++;
        if (timeStamp <= 0) // Prevent overflow
        {
            timeStamp = 1;
        }
    }

    public int GetTimeStamp()
    {
        return timeStamp;
    }
}
