using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : MonoBehaviour
{
    public GameObject healFX;
    bool isHealing = false;
    GameObject fx;
    public void Heal()
    {
        if (!isHealing)
        {
            fx = Instantiate(healFX, transform.Find("FXPos"));
            isHealing = true;
            Invoke("RemoveHealFX", 1.9f);
        } 
    }
    void RemoveHealFX()
    {
        Destroy(fx);
        isHealing = false;
    }
}
// coroutine 버전