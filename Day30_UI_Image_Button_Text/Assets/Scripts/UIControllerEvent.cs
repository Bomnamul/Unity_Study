using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerEvent : MonoBehaviour
{
    public Health leftSide;
    public Button healButton;
    public Button damageButton;
    public Text timerText;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageButton.onClick.AddListener(() => GameDataManagerEvent.Instance.TakeDamage(10f));
        healButton.onClick.AddListener(() => GameDataManagerEvent.Instance.Heal(20f));
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        int timeCount = GameDataManagerEvent.Instance.GetTimeCount();
        while (timeCount >= 0)
        {
            GameDataManagerEvent.Instance.SetTimeCount(--timeCount);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnEnable()
    {
        GameDataManagerEvent.Instance.OnDataChanged += OnDataUpdate;
    }

    private void OnDisable()
    {
        if (GameDataManagerEvent.Instance != null)
        {
            GameDataManagerEvent.Instance.OnDataChanged -= OnDataUpdate;
        }
    }

    // Update is called once per frame
    void OnDataUpdate()
    {
        //int timeStamp = GameDataManagerEvent.Instance.GetTimeStamp();
        //if (timeStamp != uiTimeStamp)
        {
            //    uiTimeStamp = timeStamp;
            float currentHealth = GameDataManagerEvent.Instance.GetCurrentHealth();
            float maxHealth = GameDataManagerEvent.Instance.GetMaxHealth();
            leftSide.UpdateHealthBar(currentHealth, maxHealth);

            int timeCount = GameDataManagerEvent.Instance.GetTimeCount();
            timerText.text = timeCount.ToString();
        }
    }
}
