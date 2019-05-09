using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlatform : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        GameObject c = collision.collider.gameObject;
        if (c.CompareTag("Player"))
        {
            c.GetComponent<HealOverTime>().Heal();
        }
    }
}
