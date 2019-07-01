using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject itemButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var s in collision.GetComponent<Inventory>().slots)
            {
                if (s.transform.childCount == 0)
                {
                    Instantiate(itemButton, s.transform);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
