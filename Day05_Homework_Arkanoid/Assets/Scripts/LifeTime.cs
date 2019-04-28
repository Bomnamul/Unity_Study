using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    private float lifeTime = 1000;

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime * 1000;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
