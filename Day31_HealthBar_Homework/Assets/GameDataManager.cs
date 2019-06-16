using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    protected GameDataManager() { }

    float prevHealth;
    float currentHealth = 4f;
    float maxHealth = 4f;

    int timeStamp = 0;

    public void TakeDamage(float amount)
    {
        prevHealth = currentHealth;
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }
        UpdateTimeStamp();
    }

    public void Heal(float amount)
    {
        prevHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateTimeStamp();
    }

    public float GetPrevHealth()
    {
        return prevHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
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
