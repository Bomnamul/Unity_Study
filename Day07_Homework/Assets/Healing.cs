using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public ParticleSystem heal;
    
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(heal, collision.transform.position, Quaternion.Euler(-90f, 0f, 0f));
    }
}
