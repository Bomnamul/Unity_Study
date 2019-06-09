using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image HealthBar;
    public Image HealthGlow;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        HealthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateHealthBarGlow(float currentHealth, float maxHealth)
    {
        StartCoroutine(Glow(currentHealth, maxHealth));
    }

    IEnumerator Glow(float currentHealth, float maxHealth)
    {
        float current = currentHealth;
        HealthGlow.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        while (current <= maxHealth)
        {
            HealthGlow.fillAmount = current;
            current++;
            yield return null;
        }
    }
}
