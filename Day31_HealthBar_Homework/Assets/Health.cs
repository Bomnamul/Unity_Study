using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image HealthBar;
    public Image HealthGlow;

    float timer = 0f;

    public void UpdateHealthBar(float prevHealth, float currentHealth, float maxHealth)
    {
        StartCoroutine(Glow(prevHealth, currentHealth, maxHealth));
    }

    IEnumerator Glow(float prevHealth, float currentHealth, float maxHealth)
    {
        Debug.Log("Coroutine Worked");
        HealthGlow.gameObject.SetActive(true);
        if (prevHealth > currentHealth)
        {
            Debug.Log("Damaged");
            HealthBar.fillAmount = currentHealth / maxHealth;
            HealthGlow.fillAmount = prevHealth / maxHealth;
            yield return new WaitForSeconds(0.5f);
            while (HealthBar.fillAmount != HealthGlow.fillAmount)
            {
                timer += Time.deltaTime / 0.25f;
                HealthGlow.fillAmount = Mathf.Lerp(prevHealth / maxHealth, currentHealth / maxHealth, timer);
                yield return null;
            }
        }
        else if(prevHealth < currentHealth)
        {
            Debug.Log("Healed");
            HealthGlow.fillAmount = currentHealth / maxHealth;
            HealthBar.fillAmount = prevHealth / maxHealth;
            yield return new WaitForSeconds(0.5f);
            while (HealthBar.fillAmount != HealthGlow.fillAmount)
            {
                timer += Time.deltaTime / 0.25f;
                HealthBar.fillAmount = Mathf.Lerp(prevHealth / maxHealth, currentHealth / maxHealth, timer);
                yield return null;
            }
        }
        timer = 0f;
        HealthGlow.gameObject.SetActive(false);
    }
}
