using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickEvent : MonoBehaviour
{
    public GameObject explosion;
    private void OnCollisionEnter(Collision collision)
    {
        ScoreManager.score += 150;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
