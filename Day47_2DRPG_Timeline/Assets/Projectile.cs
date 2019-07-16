using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
