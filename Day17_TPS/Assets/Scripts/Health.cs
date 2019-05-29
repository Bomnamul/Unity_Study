using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public const int maxHealth = 100;
    public float currentHealth = maxHealth;
    public RectTransform foreground;

    // Start is called before the first frame update
    void Start()
    {
        RedrawHealthBar();
    }

    private void RedrawHealthBar()
    {
        foreground.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }

    public void DecreaseHP(float amount)
    {
        if (currentHealth <= 0f)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }
        RedrawHealthBar();
    }

    public void IncreaseHP(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        RedrawHealthBar();
    }
}
