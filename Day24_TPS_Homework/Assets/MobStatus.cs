using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatus : MonoBehaviour
{
    float hp = 100f;
    public Transform hpBar;

    public void GetDamaged(float damage)
    {
        hp -= damage;
        hpBar.localScale -= new Vector3(damage * 0.01f, 0f, 0f);
        print(name + " HP: " + hp);

        if (hpBar.localScale.x <= 0f)
        {
            hpBar.localScale = new Vector3(0f, 1f, 1f);
            gameObject.SetActive(false);
            return;
        }
    }
}
